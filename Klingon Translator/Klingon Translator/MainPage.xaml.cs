using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Klingon_Translator.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Klingon_Translator {

    public partial class MainPage : PhoneApplicationPage {

        string strLngTo = "fr";
        private static string strTextToTranslate = "";

        // Constructor
        public MainPage() {

            InitializeComponent();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            // Initialize the strTextToTranslate here, while we're on the UI thread
            strTextToTranslate = txtToTrans.Text;
            // STEP 1: Create the request for the OAuth service that will
            // get us our access tokens.
            String strTranslatorAccessURI =
                  "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
            System.Net.WebRequest req = System.Net.WebRequest.Create(strTranslatorAccessURI);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            // Important to note -- the call back here isn't that the request was processed by the server
            // but just that the request is ready for you to do stuff to it (like writing the details)
            // and then post it to the server.
            IAsyncResult writeRequestStreamCallback =
              (IAsyncResult)req.BeginGetRequestStream(new AsyncCallback(RequestStreamReady), req);

        }

        private void optFrench_Checked(object sender, RoutedEventArgs e) {
            strLngTo = "fr";
        }

        private void optSpanish_Checked(object sender, RoutedEventArgs e) {
            strLngTo = "es";
        }

        private void optGerman_Checked(object sender, RoutedEventArgs e) {
            strLngTo = "de";
        }


        private void RequestStreamReady(IAsyncResult ar) {
            // STEP 2: The request stream is ready. Write the request into the POST stream
            string clientID = "KlingonTranslator";
            string clientSecret = "EAhps3yi02N+PXtZwqslezbhN07a31WhAMtNT0KXT70";
            String strRequestDetails = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientID), HttpUtility.UrlEncode(clientSecret));

            // note, this isn't a new request -- the original was passed to beginrequeststream, so we're pulling a reference to it back out. It's the same request

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)ar.AsyncState;
            // now that we have the working request, write the request details into it
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strRequestDetails);
            System.IO.Stream postStream = request.EndGetRequestStream(ar);
            postStream.Write(bytes, 0, bytes.Length);
            postStream.Close();
            // now that the request is good to go, let's post it to the server
            // and get the response. When done, the async callback will call the
            // GetResponseCallback function
            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }

        private void GetResponseCallback(IAsyncResult ar) {
            // STEP 3: Process the response callback to get the token
            // we'll then use that token to call the translator service
            // Pull the request out of the IAsynch result
            HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
            // The request now has the response details in it (because we've called back having gotten the response
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
            // Using JSON we'll pull the response details out, and load it into an AdmAccess token class (defined earlier in this module)
            // IMPORTANT (and vague) -- despite the name, in Windows Phone, this is in the System.ServiceModel.Web library,
            // and not the System.Runtime.Serialization one -- so you will need to have a reference to System.ServiceModel.Web
            try {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new
                System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(AdmAccessToken));
                AdmAccessToken token =
                  (AdmAccessToken)serializer.ReadObject(response.GetResponseStream());

                string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Net.HttpUtility.UrlEncode(strTextToTranslate) + "&from=en&to=" + strLngTo;
                System.Net.WebRequest translationWebRequest = System.Net.HttpWebRequest.Create(uri);
                // The authorization header needs to be "Bearer" + " " + the access token
                string headerValue = "Bearer " + token.access_token;
                translationWebRequest.Headers["Authorization"] = headerValue;
                // And now we call the service. When the translation is complete, we'll get the callback
                IAsyncResult writeRequestStreamCallback = (IAsyncResult)translationWebRequest.BeginGetResponse(new AsyncCallback(translationReady), translationWebRequest);
            }
            catch (Exception ex) {
                // Do nothing
            }
        }

        private void translationReady(IAsyncResult ar) {
            // STEP 4: Process the translation
            // Get the request details
            HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
            // Get the response details
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
            // Read the contents of the response into a string
            System.IO.Stream streamResponse = response.GetResponseStream();
            System.IO.StreamReader streamRead = new System.IO.StreamReader(streamResponse);
            string responseString = streamRead.ReadToEnd();
            // Translator returns XML, so load it into an XDocument
            // Note -- you need to add a reference to the System.Linq.XML namespace
            System.Xml.Linq.XDocument xTranslation =
              System.Xml.Linq.XDocument.Parse(responseString);
            string strTest = xTranslation.Root.FirstNode.ToString();
            // We're not on the UI thread, so use the dispatcher to update the UI
            Deployment.Current.Dispatcher.BeginInvoke(() => lblTranslatedText.Text = strTest);

        }

        private void btnSpeak_Click(object sender, RoutedEventArgs e) {

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}
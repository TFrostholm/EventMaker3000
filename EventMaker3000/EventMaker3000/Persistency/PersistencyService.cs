using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using EventMaker3000.Model;
using Newtonsoft.Json;

namespace EventMaker3000.Persistency
{
    // Is responsible for writing and reading to the json file.
    class PersistencyService
    {
        private static string jsonFileName = "EventsAsJson1.dat";

        public static void SaveEventsAsJsonAsync(ObservableCollection<Event> events)
        {
            string eventsJsonString = JsonConvert.SerializeObject(events);
            SerializeEventsFileAsync(eventsJsonString, jsonFileName);
        }


        public static async Task<List<Event>> LoadEventsFromJsonAsync()
        {
            string eventsJsonString = await DeSerializeEventsFileAsync(jsonFileName);
            if (eventsJsonString != null)
            {
                return (List<Event>)JsonConvert.DeserializeObject(eventsJsonString, typeof(List<Event>));
            }
            return null;
        }

        public static async void SerializeEventsFileAsync(string eventsString, string fileName)
        {
            StorageFile localFile =
                await
                    ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                        CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, eventsString);
        }

        public static async Task<string> DeSerializeEventsFileAsync(String fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException exception)
            {

                MessageDialogHelper.Show(
                    "Can't find any Events to show! First try adding and save some Events before you Load Events!",
                    "File not found!");
                return null;
            }
        }

        private class MessageDialogHelper
        {
            public static async void Show(string content, string header)
            {
                MessageDialog messageDialog = new MessageDialog(content, header);
                await messageDialog.ShowAsync();
            }
        }

        //Not sure if we need to save it to xml format but this can be useful anyway later on
        // Just uncomment the code below:
        //
        //private static string xmlFileName = "EventsAsXml1.dat";

        //public static async void SaveEventsAsXmlAsync(ObservableCollection<Event> events)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(events.GetType());
        //    StringWriter textWriter = new StringWriter();
        //    xmlSerializer.Serialize(textWriter, events);
        //    SerializeEventsFileAsync(textWriter.ToString(), xmlFileName);
        //}

        //
    }
}
using MobileBMKG.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MobileBMKG.Services
{
    public class AutoGempaServices : IDataStore<Gempa>
    {
        public async Task<Gempa> GetAutoGempaAsync()
        {
            var autoGempa = await GetData<InfoGempa>("http://data.bmkg.go.id/autogempa.xml");
            return autoGempa.Gempa.FirstOrDefault();
        }
        public async Task<Gempa> LastGempaDirasakanAsync()
        {
            var last = await GetData<InfoGempa>("http://data.bmkg.go.id/lastgempadirasakan.xml");
            if (last != null)
                return last.Gempa.FirstOrDefault();
            else
                return null;
        }

        public async Task<List<Gempa>> GetGempaDirasakanAsync()
        {
            var dirasakan = await GetData<InfoGempa>("http://data.bmkg.go.id/gempadirasakan.xml");
            return dirasakan.Gempa;
        }

        public async Task<List<Gempa>> GetGempaTerkiniAsync()
        {
            var terkini = await GetData<InfoGempa>("http://data.bmkg.go.id/gempaterkini.xml");
            return terkini.Gempa;
        }


        public async Task<Infotsunami> GetLastTsunamiAsync()
        {
            var tsunami = await GetData<Infotsunami>("http://data.bmkg.go.id/lasttsunami.xml");
            return tsunami;
        }
        private Task<T> GetData<T>(string Url)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string URLString = Url;
                string xmlStr;
                using (var wc = new WebClient())
                {
                    xmlStr = wc.DownloadString(URLString);
                }
                var xml = xmlStr.Replace("Gempa", "gempa");
                var last = (T)ObjectToXML(xml, typeof(T));
                return Task.FromResult(last);
                //   return resut;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public Object ObjectToXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception)
            {
                //Handle Exception Code
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return obj;
        }

     
       
    }
}

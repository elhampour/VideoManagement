using System.Xml;

namespace VideoManagement.Models
{
    public static class VastExtensions
    {
        public static VastCreationResult CreateVast(this CreateVideoAdVm vm, string filePath)
        {
            switch (vm.VastVersion)
            {
                case VastVersion.Version2:
                    return CreateVastVersion2(vm, filePath);
                case VastVersion.Version3:
                    return new VastCreationResult()
                    {
                        VastCreationResultType = VastCreationResultType.Failure,
                        Message = "Vast Version 3 is not supported yet."
                    };
                case VastVersion.Version4:
                    return new VastCreationResult()
                    {
                        VastCreationResultType = VastCreationResultType.Failure,
                        Message = "Vast Version 4 is not supported yet."
                    };
                default:
                    return new VastCreationResult()
                    {
                        VastCreationResultType = VastCreationResultType.Failure,
                        Message = "Unhandled situation"
                    };
            }
        }

        public static VastCreationResult CreateVastVersion2(CreateVideoAdVm vm, string filePath)
        {
            var version = "2.0";

            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "no");
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement vastElement = doc.CreateElement(string.Empty, "vast", string.Empty);
            vastElement.SetAttribute("version", version);
            vastElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            vastElement.SetAttribute("xsi:noNamespaceSchemaLocation", "vast.xsd");
            doc.AppendChild(vastElement);

            XmlElement adElement = doc.CreateElement(string.Empty, "Ad", string.Empty);
            adElement.SetAttribute("id", "1");
            vastElement.AppendChild(adElement);

            XmlElement inLineElement = doc.CreateElement(string.Empty, "InLine", string.Empty);
            adElement.AppendChild(inLineElement);

            XmlElement adSystemElement = doc.CreateElement(string.Empty, "AdSystem", string.Empty);
            XmlText text1 = doc.CreateTextNode("Ninisite");
            adSystemElement.AppendChild(text1);
            inLineElement.AppendChild(adSystemElement);

            XmlElement adTitleElement = doc.CreateElement(string.Empty, "AdTitle", string.Empty);
            XmlText text2 = doc.CreateTextNode(vm.Title);
            adTitleElement.AppendChild(text2);
            inLineElement.AppendChild(adTitleElement);

            XmlElement descriptionElement = doc.CreateElement(string.Empty, "Description", string.Empty);
            XmlText text3 = doc.CreateTextNode(vm.Description);
            descriptionElement.AppendChild(text3);
            inLineElement.AppendChild(descriptionElement);

            XmlElement impressionElement = doc.CreateElement(string.Empty, "Impression", string.Empty);
            impressionElement.SetAttribute("id", "impression1");

            XmlText impressionLinkText = doc.CreateTextNode("<![CDATA[http://servedby.flashtalking.com/imp/1/31714;812030;201;gif;DailyMail;640x360VASTHTML5/?ft_creative=377314&ft_configuration=0&cachebuster=1871565385]]>");
            impressionElement.AppendChild(impressionLinkText);
            impressionElement.InnerXml = HttpUtility.HtmlDecode(impressionElement.InnerXml);
            inLineElement.AppendChild(impressionElement);

            XmlElement creativesElement = doc.CreateElement(string.Empty, "Creatives", string.Empty);
            inLineElement.AppendChild(creativesElement);

            XmlElement creativeElement = doc.CreateElement(string.Empty, "Creative", string.Empty);
            creativeElement.SetAttribute("sequence", "1");
            creativesElement.AppendChild(creativeElement);

            XmlElement linearElement = doc.CreateElement(string.Empty, "Linear", string.Empty);
            creativeElement.AppendChild(linearElement);

            XmlElement durationElement = doc.CreateElement(string.Empty, "Duration", string.Empty);
            XmlText durationText = doc.CreateTextNode("00:00:15");
            durationElement.AppendChild(durationText);
            linearElement.AppendChild(durationElement);

            XmlElement trackingEventsElement = doc.CreateElement(string.Empty, "TrackingEvents", string.Empty);
            linearElement.AppendChild(trackingEventsElement);

            var trackingEvents = EnumExtension.ToList<EnumItem>(typeof(TrackingType));

            foreach (var item in trackingEvents)
            {
                XmlElement itemTrcking = doc.CreateElement(string.Empty, "Tracking", string.Empty);
                itemTrcking.SetAttribute("event", item.Text.ToLower());
                XmlText itemEventLinkText = doc.CreateTextNode(string.Format("<![CDATA[http://localhost:62046/stat/{0}]]>", item.Id));
                itemTrcking.AppendChild(itemEventLinkText);
                itemTrcking.InnerXml = HttpUtility.HtmlDecode(itemTrcking.InnerXml);
                trackingEventsElement.AppendChild(itemTrcking);
            }

            XmlElement videoClicksElement = doc.CreateElement(string.Empty, "VideoClicks", string.Empty);
            linearElement.AppendChild(videoClicksElement);

            XmlElement clickThroughElement = doc.CreateElement(string.Empty, "ClickThrough", string.Empty);
            XmlText clickThroughLinkText = doc.CreateTextNode("<![CDATA[http://servedby.flashtalking.com/click/1/31714;812030;377314;211;0/?random=1871565385&ft_width=640&ft_height=360&url=http://www.google.co.uk]]>");
            clickThroughElement.AppendChild(clickThroughLinkText);
            clickThroughElement.InnerXml = HttpUtility.HtmlDecode(clickThroughElement.InnerXml);
            videoClicksElement.AppendChild(clickThroughElement);

            XmlElement mediaFilesElement = doc.CreateElement(string.Empty, "MediaFiles", string.Empty);
            linearElement.AppendChild(mediaFilesElement);

            XmlElement mediaFileElement = doc.CreateElement(string.Empty, "MediaFile", string.Empty);
            mediaFileElement.SetAttribute("id", "1");
            mediaFileElement.SetAttribute("delivery", "progressive");
            mediaFileElement.SetAttribute("type", "video/mp4");
            mediaFileElement.SetAttribute("bitrate", "524");
            mediaFileElement.SetAttribute("width", "640");
            mediaFileElement.SetAttribute("height", "360");
            XmlText mediaFileLinkText = doc.CreateTextNode("<![CDATA[http://cdn.flashtalking.com/17601/30988_26752_WacoClub_640x360_384kbps.mp4]]>");
            mediaFileElement.AppendChild(mediaFileLinkText);
            mediaFileElement.InnerXml = HttpUtility.HtmlDecode(mediaFileElement.InnerXml);
            mediaFilesElement.AppendChild(mediaFileElement);

            doc.Save(filePath);

            string html = System.IO.File.ReadAllText(filePath);

            return new VastCreationResult();
        }
    }
}
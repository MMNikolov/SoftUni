using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Client")]
    public class ExportClientDTO
    {
        [XmlElement("ClientName")]
        public string ClientName { get; set; }

        [XmlElement("VatNumber")]
        public string VatNumber { get; set; }

        [XmlAttribute("InvoicesCount")]
        public int InvoicesCount { get; set; }

        [XmlArray("Invoices")]
        public ExportInvoiceDTO[] Invoices { get; set; }
    }
}

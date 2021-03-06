﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    [DataContract]
    public class ProductResponse : BaseAgroContract
    {
        [DataMember]
        public List<Product> Products { get; set; }
    }

    [DataContract]
    public class Product {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
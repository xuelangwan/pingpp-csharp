﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Pingpp.Net;
using Pingpp.Exception;

namespace Pingpp.Models
{
    public class Charge : Pingpp
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public int? Created { get; set; }

        [JsonProperty("livemode")]
        public bool Livemode { get; set; }

        [JsonProperty("paid")]
        public bool Paid { get; set; }

        [JsonProperty("refunded")]
        public bool Refunded { get; set; }

        [JsonProperty("app")]
        public string App { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("order_no")]
        public string OrderNo { get; set; }

        [JsonProperty("client_ip")]
        public string ClientIp { get; set; }

        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [JsonProperty("amount_settle")]
        public int? AmountSettle { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("extra")]
        public Dictionary<string, object> Extra { get; set; }

        [JsonProperty("time_paid")]
        public int? TimePaid { get; set; }

        [JsonProperty("time_expire")]
        public int? TimeExpire { get; set; }

        [JsonProperty("time_settle")]
        public int? TimeSettle { get; set; }

        [JsonProperty("transaction_no")]
        public string TransactionNo { get; set; }

        [JsonProperty("refunds")]
        public RefundList Refunds { get; set; }

        [JsonProperty("amount_refunded")]
        public int? AmountRefunded { get; set; }

        [JsonProperty("failure_code")]
        public string FailureCode { get; set; }

        [JsonProperty("failure_msg")]
        public string FailureMsg { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, object> Metadata { get; set; }

        [JsonProperty("credential")]
        public object Credential { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        private const string BaseUrl = "/v1/charges";

        public static Charge Create(Dictionary<string, object> chParams)
        {
            var ch = Requestor.DoRequest(BaseUrl, "POST", chParams, false);
            return Mapper<Charge>.MapFromJson(ch);
        }

        public static Charge Retrieve(string id)
        {
            var url = string.Format("{0}/{1}", BaseUrl, id);
            var ch = Requestor.DoRequest(url, "GET");
            return Mapper<Charge>.MapFromJson(ch);
        }

        public static ChargeList List(Dictionary<string, object> listParams = null)
        {
            object value;
            if (listParams != null && listParams.TryGetValue("app", out value))
            {
                var app_id = value as Dictionary<string, string>;
                string id;
                if (app_id != null && app_id.TryGetValue("id", out id))
                {
                    if (String.IsNullOrEmpty(id))
                    {
                        throw new PingppException("Please pass app[id] as param");
                    }
                }
            }
            else
            {
                throw new PingppException("Please pass app[id] as param");
            }

            var url = Requestor.FormatUrl(BaseUrl, Requestor.CreateQuery(listParams));
            var ch = Requestor.DoRequest(url, "GET");
            return Mapper<ChargeList>.MapFromJson(ch);
        }

    }

}

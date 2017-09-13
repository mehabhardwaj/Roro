﻿// BSD 2-Clause License

// Copyright(c) 2017, Arvie Delgado
// All rights reserved.

// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:

// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.

// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.

// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Linq;

namespace OpenRPA.Queries
{
    public abstract class Element
    {
        public Query GetQuery()
        {
            var query = new Query();
            var props = this.GetType().GetProperties().Where(attr => Attribute.IsDefined(attr, typeof(BotPropertyAttribute)));
            foreach (var prop in props)
            {
                query.Append(prop.Name, prop.GetValue(this));
            }
            return query;
        }

        public bool TryQuery(Query query)
        {
            foreach (var prop in query)
            {
                if (this.GetType().GetProperty(prop.Name).GetValue(this).Equals(prop.Value))
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        //public string Serialize()
        //{
        //    Console.Clear();
        //    var sb = new StringBuilder();

        //    var props = this.GetType()
        //        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        //        .Where(attr => Attribute.IsDefined(attr, typeof(BotPropertyAttribute)));

        //    var xdoc = new XDocument();
        //    xdoc.Add(new XElement(this.GetType().Name));
        //    foreach (var prop in props)
        //    {
        //        xdoc.Root.Add(
        //            new XElement(prop.MemberType.ToString(),
        //                new XAttribute("Enabled", true.ToString()),
        //                new XElement("Name", prop.Name),
        //                new XElement("Type", prop.PropertyType),
        //                new XElement("Value", prop.GetValue(this))
        //            )
        //        );
        //    }

        //    return xdoc.ToString();
        //}

        //public static T Deserialize(string text)
        //{
        //    if (text == null)
        //        throw new ArgumentNullException("text");

        //var xs = new XmlSerializer(this.GetType());
        //xs.Serialize(writer, this);
        //return writer.ToString();

        //    using (var reader = new StringReader(text))
        //    {
        //        var xs = new XmlSerializer(typeof(T));
        //        return (T)xs.Deserialize(reader);
        //    }
        //}
    }
}
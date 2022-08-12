/*
    Copyright © 2003 RiskCare Ltd. All rights reserved.
    Copyright © 2010 SvgNet & SvgGdi Bridge Project. All rights reserved.
    Copyright © 2015-2022 Rafael Teixeira, Mojmír Němeček, Benjamin Peterson and Other Contributors

    Original source code licensed with BSD-2-Clause spirit, treat it thus, see accompanied LICENSE for more
*/

using System.Xml;

using SvgNet.Types;

namespace SvgNet.Elements;
/// <summary>
/// This is an SvgElement that can have a CSS style and an SVG transformation list.  It contains special properties to make reading and setting the style
/// and the transformation easier.  All SVG elements that actually represent visual entities or groups of entities are <c>SvgStyledTransformedElements</c>.
/// </summary>
public class SvgStyledTransformedElement : SvgElement {
    public SvgStyledTransformedElement() {
    }

    public SvgStyledTransformedElement(string id) : base(id) {
    }

    /// <summary>
    /// Provides an easy way to get the attribute called "style" as an <c>SvgStyle</c> object.  If no such attribute has been set, one is created when
    /// this property is read.
    /// </summary>
    public SvgStyle Style {
        get => GetTypedAttribute("style", (obj) => new SvgStyle(obj.ToString()));
        set => _atts["style"] = value;
    }

    /// <summary>
    /// Provides an easy way to get the attribute called "transform" as an <c>SvgTransformList</c> object.  If no such attribute has been set, one is created when
    /// this property is read.
    /// </summary>
    public SvgTransformList Transform {
        get => GetTypedAttribute("transform", (obj) => new SvgTransformList(obj.ToString()));
        set => _atts["transform"] = value;
    }

    /// <summary>
    /// Given a document and a current node, read this element from the node.
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="el"></param>
    public override void ReadXmlElement(XmlDocument doc, XmlElement el) {
        foreach (XmlAttribute att in el.Attributes) if (att.Name == "style") Style = new SvgStyle(att.Value);
            else if (att.Name == "transform") Transform = new SvgTransformList(att.Value);
            else this[att.Name] = att.Value;
    }

    /// <summary>
    /// Overridden in this class to provide special handling for the style and transform attributes,
    /// which are often long and complicated.  For instance, it may be desirable for styles to be written as entities or as separate
    /// attributes.
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="parent"></param>
    public override void WriteXmlElements(XmlDocument doc, XmlElement parent) {
        XmlElement me = doc.CreateElement("", Name, doc.NamespaceURI);
        foreach (string s in _atts.Keys) {
            object attribute = _atts[s];
            if (attribute != null) if (s == "style") WriteStyle(doc, me, attribute);
                else if (s == "transform") WriteTransform(doc, me, attribute);
                else {
                    // xlink qualified attributes are an special case because they need an special namespace
                    (bool isPrefixed, string localName) = s.IsPrefixedBy("xlink:");
                    _ = isPrefixed
                        ? me.SetAttribute(localName, xlinkNamespaceURI, _atts[s].ToString())
                        : me.SetAttribute(s, doc.NamespaceURI, _atts[s].ToString());
                }
        }

        foreach (SvgElement el in Children) el.WriteXmlElements(doc, me);

        _ = parent == null ? doc.AppendChild(me) : parent.AppendChild(me);
    }

    private static void WriteStyle(XmlDocument doc, XmlElement me, object o) {
        if (o.GetType() != typeof(SvgStyle)) {
            _ = me.SetAttribute("style", doc.NamespaceURI, o.ToString());
            return;
        }

        var style = (SvgStyle)o;

        /*
        foreach(string s in style.Keys)
        {
            me.SetAttribute(s, doc.NamespaceURI, style.Get(s).ToString());
        }
        */

        _ = me.SetAttribute("style", doc.NamespaceURI, style.ToString());

        _ = doc.CreateEntityReference("pingu");
    }

    private static void WriteTransform(XmlDocument doc, XmlElement me, object o) =>
        //if (o.GetType() != typeof(SvgTransformList))
        //{
        me.SetAttribute("transform", doc.NamespaceURI, o.ToString());//}
}

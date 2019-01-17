﻿/*
    Copyright © 2003 RiskCare Ltd. All rights reserved.
    Copyright © 2010 SvgNet & SvgGdi Bridge Project. All rights reserved.
    Copyright © 2015-2019 Rafael Teixeira, Mojmír Němeček, Benjamin Peterson and Other Contributors

    Original source code licensed with BSD-2-Clause spirit, treat it thus, see accompanied LICENSE for more
*/

using System;
using System.Globalization;

namespace SvgNet.SvgTypes
{
    /// <summary>
    /// A length or coordinate component (in SVG 1.1 the specification says they are the same)
    /// </summary>
    public class SvgLength : ICloneable
    {
        public SvgLength(string s) => FromString(s);

        public SvgLength(float f)
        {
            Value = f;
            Type = SvgLengthType.SVG_LENGTHTYPE_UNKNOWN;
        }

        public SvgLength(float f, SvgLengthType type)
        {
            Value = f;
            Type = type;
        }

        public SvgLengthType Type { get; set; }

        public float Value { get; set; }

        public static implicit operator SvgLength(string s)
        {
            return new SvgLength(s);
        }

        public static implicit operator SvgLength(float s)
        {
            return new SvgLength(s);
        }

        public object Clone() => new SvgLength(Value, Type);

        public void FromString(string s)
        {
            var i = s.LastIndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            if (i == -1)
                return;

            Value = float.Parse(s.Substring(0, i + 1), CultureInfo.InvariantCulture);

            switch (s.Substring(i + 1)) {
                case "%":
                    Type = SvgLengthType.SVG_LENGTHTYPE_PERCENTAGE;
                    break;

                case "em":
                    Type = SvgLengthType.SVG_LENGTHTYPE_EMS;
                    break;

                case "ex":
                    Type = SvgLengthType.SVG_LENGTHTYPE_EXS;
                    break;

                case "px":
                    Type = SvgLengthType.SVG_LENGTHTYPE_PX;
                    break;

                case "cm":
                    Type = SvgLengthType.SVG_LENGTHTYPE_CM;
                    break;

                case "mm":
                    Type = SvgLengthType.SVG_LENGTHTYPE_MM;
                    break;

                case "in":
                    Type = SvgLengthType.SVG_LENGTHTYPE_IN;
                    break;

                case "pt":
                    Type = SvgLengthType.SVG_LENGTHTYPE_PT;
                    break;

                case "pc":
                    Type = SvgLengthType.SVG_LENGTHTYPE_PC;
                    break;

                case "":
                    Type = SvgLengthType.SVG_LENGTHTYPE_UNKNOWN;
                    break;

                default:
                    throw new SvgException("Invalid SvgLength", s);
            }
        }

        public override string ToString()
        {
            var s = Value.ToString("F", CultureInfo.InvariantCulture);
            switch (Type) {
                case SvgLengthType.SVG_LENGTHTYPE_PERCENTAGE:
                    s += "%";
                    break;

                case SvgLengthType.SVG_LENGTHTYPE_EMS:
                    s += "em";
                    break;

                case SvgLengthType.SVG_LENGTHTYPE_EXS:
                    s += "ex";
                    break;

                case SvgLengthType.SVG_LENGTHTYPE_PX:
                    s += "px";
                    break;

                case SvgLengthType.SVG_LENGTHTYPE_CM:
                    s += "cm";
                    break;

                case SvgLengthType.SVG_LENGTHTYPE_MM:
                    s += "mm";
                    break;

                case SvgLengthType.SVG_LENGTHTYPE_IN:
                    s += "in";
                    break;

                case SvgLengthType.SVG_LENGTHTYPE_PT:
                    s += "pt";
                    break;

                case SvgLengthType.SVG_LENGTHTYPE_PC:
                    s += "pc";
                    break;
            }
            return s;
        }
    }
}
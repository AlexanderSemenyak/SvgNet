﻿/*
    Copyright © 2003 RiskCare Ltd. All rights reserved.
    Copyright © 2010 SvgNet & SvgGdi Bridge Project. All rights reserved.
    Copyright © 2015-2022 Rafael Teixeira, Mojmír Němeček, Benjamin Peterson and Other Contributors

    Original source code licensed with BSD-2-Clause spirit, treat it thus, see accompanied LICENSE for more
*/

namespace SvgNet.Exceptions;

[Serializable]
public sealed class EmfException : Exception {
    public EmfException(string message)
        : base(message) {
    }

    public EmfException() : base() {
    }

    public EmfException(string message, Exception innerException) : base(message, innerException) {
    }
}


﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityWcfExtensionsTestClient.Service
{
    /// <summary>
    /// Simple service interface to test the UnityContextChannelExtension.
    /// </summary>
    public interface IContextChannelService
    {
        /// <summary>
        /// Returns the number of calls across this service's lifetime.
        /// </summary>
        /// <returns>The number of calls across this service's lifetime.</returns>
        string GetData();
    }
}

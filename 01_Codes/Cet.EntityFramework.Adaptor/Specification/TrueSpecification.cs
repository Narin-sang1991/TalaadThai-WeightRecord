﻿//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================
    using System;
    using System.Linq.Expressions;

namespace Cet.EntityFramework.Adaptor
{


    /// <summary>
    /// True specification
    /// </summary>
    /// <typeparam name="TValueObject">Type of entity in this specification</typeparam>
    public sealed class TrueSpecification<TEntity>
        :Specification<TEntity>
        where TEntity:class
    {
        #region Specification overrides

        /// <summary>
        /// <see cref=" Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.Specification{TValueObject}"/>
        /// </summary>
        /// <returns><see cref=" Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.Specification{TValueObject}"/></returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            //Create "result variable" transform adhoc execution plan in prepared plan
            //for more info: http://geeks.ms/blogs/unai/2010/07/91/ef-4-0-performance-tips-1.aspx
            bool result = true;

            Expression<Func<TEntity, bool>> trueExpression = t => result;
            return trueExpression;
        }

        #endregion
    }

    public sealed class FalseSpecification<TEntity>
        : Specification<TEntity>
        where TEntity : class
    {
        #region Specification overrides

        /// <summary>
        /// <see cref=" Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.Specification{TValueObject}"/>
        /// </summary>
        /// <returns><see cref=" Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.Specification{TValueObject}"/></returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            //Create "result variable" transform adhoc execution plan in prepared plan
            //for more info: http://geeks.ms/blogs/unai/2010/07/91/ef-4-0-performance-tips-1.aspx
            bool result = false;

            Expression<Func<TEntity, bool>> falseExpression = t => result;
            return falseExpression;
        }

        #endregion
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Northwind.Server.BusinessLayer.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Northwind.Server.BusinessLayer.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid customer id ({0})..
        /// </summary>
        public static string OrderService_AddAsync_Invalid_customer_id___0___ {
            get {
                return ResourceManager.GetString("OrderService_AddAsync_Invalid_customer_id___0___", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid employee id ({0})..
        /// </summary>
        public static string OrderService_AddAsync_Invalid_employee_id___0___ {
            get {
                return ResourceManager.GetString("OrderService_AddAsync_Invalid_employee_id___0___", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid order..
        /// </summary>
        public static string OrderService_AddAsync_Invalid_order_ {
            get {
                return ResourceManager.GetString("OrderService_AddAsync_Invalid_order_", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid shipper id ({0})..
        /// </summary>
        public static string OrderService_AddAsync_Invalid_shipper_id___0___ {
            get {
                return ResourceManager.GetString("OrderService_AddAsync_Invalid_shipper_id___0___", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The order with id = {0} not found..
        /// </summary>
        public static string OrderService_FindAsync_ {
            get {
                return ResourceManager.GetString("OrderService_FindAsync_", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid order id ({0})..
        /// </summary>
        public static string OrderService_FindAsync_Invalid_order_id___0___ {
            get {
                return ResourceManager.GetString("OrderService_FindAsync_Invalid_order_id___0___", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The category with id = {0} not found..
        /// </summary>
        public static string OrderService_GetOrdersWithDetailsAsync_InvalidCategoryId {
            get {
                return ResourceManager.GetString("OrderService_GetOrdersWithDetailsAsync_InvalidCategoryId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The orders by category with id = {0} not found..
        /// </summary>
        public static string OrderService_GetOrdersWithDetailsAsync_OrdersNotFound {
            get {
                return ResourceManager.GetString("OrderService_GetOrdersWithDetailsAsync_OrdersNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The orders not found..
        /// </summary>
        public static string OrderService_ListAsync_The_orders_not_found_ {
            get {
                return ResourceManager.GetString("OrderService_ListAsync_The_orders_not_found_", resourceCulture);
            }
        }
    }
}

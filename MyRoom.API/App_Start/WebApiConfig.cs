﻿using MyRoom.Model;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Mvc;

namespace MyRoom.API
{
    public static class WebApiConfig
    {
        public static string ControllerOnly = "ApiControllerOnly";
        public static string ControllerAndId = "ApiControllerAndIntegerId";
        public static string ControllerAction = "ApiControllerAction";

        private static void ODataConfigure(HttpConfiguration config)
        {
            // Web API configuration and services, route odata
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //   builder.EntitySet<User>("User");

            builder.ComplexType<Translation>();
            builder.ComplexType<Catalog>();
            builder.ComplexType<Module>();
            builder.ComplexType<Category>();
            builder.ComplexType<Product>();

           // builder.EntitySet<RelUserAccess>("RelUserAccess");
         //   builder.EntitySet<RelUserCatalogue>("RelUserCatalogues");
         //   builder.EntitySet<RelUserCategory>("RelUserCategory");
            //builder.EntitySet<RelUserHotel>("RelUserHotels");
           // builder.EntitySet<RelUserModule>("RelUserModules");
            //builder.EntitySet<RelUserProduct>("RelUserProducts");
           // builder.EntitySet<ActiveHotelCatalogue>("ActiveHotelCatalogue");
           // builder.EntitySet<Catalog>("Catalogues");
           // builder.EntitySet<ActiveHotelCategory>("ActiveHotelCategories");
            //builder.EntitySet<Category>("Categories");
            //builder.EntitySet<ActiveHotelModule>("ActiveHotelModule");
         //   builder.EntitySet<Module>("Modules");
           // builder.EntitySet<Hotel>("Hotels");
           // builder.EntitySet<ActiveHotelProduct>("ActiveHotelProduct");
          //  builder.EntitySet<Product>("Products");
            //  builder.EntitySet<RelCatalogueModule>("RelCatalogueModules");
         //   builder.EntitySet<Translation>("Translations");
          //  builder.EntitySet<RelCategoryProduct>("RelCategoryProducts");
          //  builder.EntitySet<RelModuleCategory>("RelModuleCategory");
           // builder.EntitySet<MenuAccess>("MenuAccess");
           // builder.EntitySet<RelatedProducts>("RelatedProducts");
        //    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());


            config.MapHttpAttributeRoutes();

        }

        public static void Register(HttpConfiguration config)
        {
            ODataConfigure(config);

            var routes = config.Routes;

            //var cors = new EnableCorsAttribute("http://management-webapi-myroom.azurewebsites.net", "*", "*");
            //config.EnableCors(cors);

            //PAPA: This was the default route. I removed this and replaced with theones below.
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // This controller-per-type route is ideal for GetAll calls.
            // It finds the method on the controller using WebAPI conventions
            // The template has no parameters.
            //
            // ex: api/sessionbriefs
            // ex: api/sessions
            // ex: api/persons
            routes.MapHttpRoute(
                name: ControllerOnly,
                routeTemplate: "api/{controller}"
            );

            // This is the default route that a "File | New MVC 4 " project creates.
            // (I changed the name, removed the defaults, and added the constraints)
            //
            // This controller-per-type route lets us fetch a single resource by numeric id
            // It finds the appropriate method GetById method
            // on the controller using WebAPI conventions
            // The {id} is not optional, must be an integer, and 
            // must match a method with a parameter named "id" (case insensitive)
            //
            //  ex: api/sessions/1
            //  ex: api/hotels/1
            routes.MapHttpRoute(
                name: ControllerAndId,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"^\d+$" } // id must be all digits
            );

            /********************************************************
            * The integer id constraint is necessary to distinguish 
            * the {id} route above from the {action} route below.
            * For example, the route above handles
            *     "api/sessions/1" 
            * whereas the route below handles
            *     "api/lookups/all"
            ********************************************************/

            // This RPC style route is great for lookups and custom calls
            // It matches the {action} to a method on the controller 
            //
            // ex: api/lookups/all
            // ex: api/lookups/rooms
            routes.MapHttpRoute(
                name: ControllerAction,
                routeTemplate: "api/{controller}/{action}"
            );

        }

    }
}

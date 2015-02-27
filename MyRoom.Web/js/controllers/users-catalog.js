app.controller('UserCatalogController', ['$scope', '$http', '$state', 'catalogService', 'toaster', '$timeout', '$filter', function ($scope, $http, $state, catalogService, toaster, $timeout, $filter) {
    $scope.saveUserCatalog = function () {
        var i = 0;
        //guardar el catalgo del user seleccionados
        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };
       
        createCatalogUserViewModel();
      
        catalogService.saveCatalogUser($scope.catalogUserVm).then(function (response) {
            $scope.toaster = { type: 'success', title: 'Success', text: 'The User Catalog has been saved' };
        },
        function (err) {
            $scope.toaster = {
                type: 'error', title: 'Error', text: err.error_description
            };
        });
        

        $scope.pop();
    }

    function createCatalogUserViewModel()
    {
       
        $scope.catalogUserVm = {
            UserId: $scope.IdUser, CatalogId: $scope.IdCatalog, Elements: createCatalog()
        }
    }


    function createCatalog()
    {
        var GetCheckedTreeNode = $filter('GetCheckedTreeNode');
        var items = GetCheckedTreeNode($scope.items, []);
        
        return items;
    }
  //  var apple_selected, tree, treedata_avm, treedata_geography;
  //  $scope.IsNewRecord = 1;
  //  $scope.sw = 1;
  //  $scope.IdUser = 0;
  //  $scope.RelUserHotel = [];
  //  $scope.users = [];
  //  $scope.hotelsSelect = [];
  //  //$scope.Catalog = [];

  //  //Juan esto lo comente
  //  //angular.element(document).ready(function () {
  //  //    $('.listbox').listbox({ 'searchbar': true });
  //  //});
  //  $scope.sourceItems = [
  //{
  //    text: "Module A",
  //    type: "module",
  //    nextsibling: "category",
  //    children: [
  //        {
  //            text: "Category A-1",
  //            type: "category",
  //            nextsibling: "category",
  //            ischeckbox: true,
  //            children: [
  //                {
  //                    text: "Category A-1-1",
  //                    type: "category",
  //                    nextsibling: "category",
  //                    ischeckbox: true,
  //                    children: [
  //                        { text: "Product B-3-1", type: "product", ischeckbox: true },
  //                        { text: "Product B-3-2", type: "product", ischeckbox: true },
  //                    ]
  //                }
  //            ]
  //        }, {
  //            text: "Category A-2",
  //            type: "category",
  //            ischeckbox: true
  //        }, {
  //            text: "Category A-3",
  //            type: "category",
  //            ischeckbox: true
  //        },
  //    ]
  //}, {
  //    text: "Module B",
  //    type: "module",
  //    children: [
  //        {
  //            text: "Category B-1", type: "category"
  //        },
  //        {
  //            text: "Category B-2", type: "category"
  //        },
  //        {
  //            text: "Category B-3",
  //            type: "category",
  //            children: [
  //                { text: "Product B-3-1", type: "product" },
  //                { text: "Product B-3-2", type: "product" },
  //            ]
  //        }
  //    ]
  //}
  //  ];

  //  apple_selected = function (branch) {
  //      return $scope.output = "APPLE! : " + branch.label;
  //  };

  //  function getChildren(id) {
  //      var getCategory = $http.get(serviceBase + "odata/RelModuleCategory(" + id + ")/Categories");
  //      var items = [];
  //      getCategory.then(function (pl) {
  //          var myObj = pl.data;
  //          var RelModuleCategory = [];

  //          RelModuleCategory = myObj.value;
  //          var i = 0;
  //          var j = $scope.Catalog.length + 1;
  //          for (i; i < RelModuleCategory.length; i++) {
  //              items[i] = {
  //                  text: RelModuleCategory[i].Name,
  //                  type: "product",
  //                  nextsibling: "category",
  //                  ischeckbox: true,
  //                  Id: RelModuleCategory[i].Id
  //              };
  //              j++;
  //          };
  //      },
  //      function (errorPl) {
  //          $log.error('failure loading users', errorPl);
  //      });

  //      return items;
  //  }


  //  //loadRecords();

  //  //function loadRecords() {

  //  //    var getUser = $http.get(serviceBase + "odata/User");

  //  //    getUser.then(function (pl) {
  //  //        var myObj = pl.data;
  //  //        $scope.users = myObj.value;
  //  //        //$scope.users.push(myObj.value);
  //  //    },
  //  //    function (errorPl) {
  //  //        $log.error('failure loading users', errorPl);
  //  //    });


  //  //    var getCatalogue = $http.get(serviceBase + "odata/Catalogues");
  //  //    getCatalogue.then(function (pl) {
  //  //        var myObj = pl.data;
  //  //        $scope.catalogues = myObj.value;
  //  //    },
  //  //    function (errorPl) {
  //  //        $log.error('failure loading catalogue', errorPl);
  //  //    });

  //  //    treedata_avm = [
  //  //        //{ label: 'Module 1' },
  //  //        //{ label: 'Module 2' },
  //  //        //{ label: 'Module 3' }
  //  //    ];

  //  //    $scope.my_data = treedata_avm;
  //  //    $scope.my_tree = tree = {};
  //  //}

  //  $scope.selectAction = function (id) {
  //      $scope.IdUser = id;
  //  };

  //  $scope.selectActionCatalog = function (id) {
  //      $scope.IdCatalog = id;
  //      $scope.Catalog = [];
  //      $scope.RelCatalogueModule = [];
  //      treedata_avm = [];                     ///odata/Modules?$expand=RelCatalogueModule
  //      var getMdules = $http.get(serviceBase + "odata/RelCatalogueModules(" + id + ")/Modules");
  //      //var getMdules = $http.get(serviceBase + "odata/Catalog");
  //      getMdules.then(function (pl) {
  //          var myObj = pl.data;
  //          $scope.RelCatalogueModule = myObj.value;
  //          var i = 0;
  //          for (i; i < $scope.RelCatalogueModule.length; i++) {
  //              $scope.Catalog[i] = { "Name": $scope.RelCatalogueModule[i].Name };
  //              $scope.sourceItems[i] = {
  //                  text: $scope.RelCatalogueModule[i].Name,
  //                  type: "module",
  //                  nextsibling: "category",
  //                  Id: $scope.RelCatalogueModule[i].Id,
  //                  ischeckbox: true,
  //                  children: getChildren($scope.RelCatalogueModule[i].Id)
  //              }
                
  //          }

  //          $scope.my_data = treedata_avm;
  //          $scope.my_tree = tree = {};
  //      },
  //      function (errorPl) {
  //          $log.error('failure loading Rel catalogue module', errorPl);
  //      });
  //  };

  //  $scope.getCatalog = function (hotel, checked) {
  //      if (checked == true) {
  //          hotel.Active = true;
  //      } else {
  //          hotel.Active = false;;
  //      }
  //  };

  //  $scope.assignPermissions = function () {

  //      //Primero guardar registro en REL_USER_CATALOGUE
        
  //      var RelUserCatalogue = {
  //          IdUser: $scope.IdUser,
  //          IdCatalogue: $scope.IdCatalog,
  //          ReadOnly: true,
  //          ReadWrite: true
  //      };
  //      var request = $http({
  //          method: "post",
  //          url: serviceBase + "odata/RelUserCatalogues",
  //          data: RelUserCatalogue
  //      });

  //      var post = request;
  //      post.then(function (pl) {

  //      }, function (err) {
  //          console.log("Err" + err);
  //      });
  //      /////////////////
  //      //Segundo guarda los registros de los modulos marcados
  //      var i = 0;
  //      for (i; i < $scope.sourceItems.length; i++) {
  //          var j = 0;
  //          if ($scope.sourceItems[i]._Selected == true)
  //          {
  //              var RelUserModule = {
  //                  IdUser: $scope.IdUser,
  //                  IdModule: $scope.sourceItems[i].Id,
  //                  ReadOnly: true,
  //                  ReadWrite: true
  //              };
  //              var request = $http({
  //                  method: "post",
  //                  url: serviceBase + "odata/RelUserModules",
  //                  data: RelUserModule
  //              });

  //              var post = request;
  //              post.then(function (pl) {

  //              }, function (err) {
  //                  console.log("Err" + err);
  //              });
 
  //          }
  //      }
  //      //Tecero guarda los registros de los categirias marcados
  //      var i = 0;
  //      for (i; i < $scope.sourceItems.length; i++) {
  //          var j = 0;
  //          for (j; j < $scope.sourceItems[i].children.length; j++) {
  //              if ($scope.sourceItems[i].children[j]._Selected == true) {
  //                  var RelUserCategory = {
  //                      IdUser: $scope.IdUser,
  //                      IdCategory: $scope.sourceItems[i].children[j].Id,
  //                      ReadOnly: true,
  //                      ReadWrite: true
  //                  };
  //                  var request = $http({
  //                      method: "post",
  //                      url: serviceBase + "odata/RelUserCategory",
  //                      data: RelUserCategory
  //                  });

  //                  var post = request;
  //                  post.then(function (pl) {

  //                  }, function (err) {
  //                      console.log("Err" + err);
  //                  });
  //              }
  //          }
  //      }
  //  };
}]);
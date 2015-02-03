/// <reference path="../../tpl/partials/user-form.html" />
(function () {
      //listbox de usuarios
      app.directive('userSelect', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/user-select.html',
              controller: function ($scope, accountService, $state) {
                  $scope.clear = function () {
                      $scope.person.selected = undefined;
                  };
                  
                  $scope.selectActionUser = function (id) {
                      $scope.IdUser = id;
                      if ($state.current.name == 'app.page.userhotel') {
                          for (i = 0; i < $scope.hotels.length; i++)
                              $scope.hotels[i].checked = false;

                          accountService.getUserHotelPermissions(id).then(function (response) {
                              $scope.permissions = response.data;
                             
                              for (i = 0; i < $scope.permissions.length; i++)
                                  $scope.hotels[$scope.permissions[i].IdHotel-1].checked = true;
                          });
                          //$("input[name='post[]']").prop('checked', '')
                          //accountService.getHotelUserId(id).then(function (response) {
                          //    $scope.HotelUser = response.data.value;
                          //    $scope.selectHotelUser = [$scope.HotelUser.length];
                          //    angular.forEach($scope.HotelUser, function (value, key) {
                          //        angular.forEach($scope.hotels, function (valuehotel, keyhotel) {
                          //            if (value.IdHotel == valuehotel.Id) {
                          //                $scope.selectHotelUser[key] = {Id: value.Id, IdHotel: valuehotel.Id};
                          //                $("input[name='post[]']").eq(keyhotel).prop('checked', 'checked')

                          //            }
                          //        })
                          //    });
                          //});
                      }
                      if ($state.current.name == 'app.page.usermenuaccess') {
                          // $("input[name='post[]']").prop('checked', '');
                          for (i = 0; i < $scope.menus.length; i++)
                              $scope.menus[i].checked = false;

                          accountService.getUserPermissions(id).then(function (response) {
                              $scope.permissions = response.data;
                             
                              for (i = 0; i < $scope.permissions.length; i++)
                                  $scope.menus[$scope.permissions[i].IdPermission-1].checked = true;

                           

                              //$scope.selectMenuUser = [$scope.MenuAccessUser.length];
                              //angular.forEach($scope.MenuAccessUser, function (value, key) {
                              //    angular.forEach($scope.menus, function (valuemenus, keymenus) {
                              //        if (value.IdPermission == valuemenus.MenuAccessId)
                              //        {
                              //            $scope.selectMenuUser[key] = { Id: value.Id, IdPermission: valuemenus.MenuAccessId };
                              //            $("input[name='post[]']").eq(keymenus).prop('checked', 'checked')
                              //        }
                              //    })
                              //});
                          });
                      }
                      
                      if ($state.current.name == 'app.page.usercatalog') {
                          //Buscar el catalogo asignado al usuario
                          accountService.getCatalogUserId(id).then(function (response) {
                              $scope.catalogUser = response.data.value;
                              $scope.activeCheckbox = true;
                              accountService.getModuleUserId(id).then(function (response) {
                                  $scope.loadTreeCatalog($scope.catalogUser[0].IdCatalogue);
                                  $scope.moduleUser = response.data.value;
                                  
                                  angular.forEach($scope.moduleUser, function (value, key) {
                                      angular.forEach($scope.items, function (valueitem, keyitem) {
                                          if (value.IdModule == valueitem.Id) {
                                              //Aca activamos el check
                                              $("input[name='post[]']").eq(keyProducts).prop('checked', 'checked')
                                          }
                                      });
                                  });
                              });
                          });
 
                      };
                  }

                  $scope.person = {};
                  accountService.getAll().then(function (response) {
                      $scope.person = response.data;

                      $scope.users = [$scope.person.length]
                      angular.forEach($scope.person, function (value, key) {
                          $scope.users[key] = {id: value.Id, name: value.Name, email: value.Email, active: value.Active, surname: value.Surname};
                      });  
                  },
                  function (err) {
                    $scope.error_description = err.error_description;
                  });
              }
          };
      })
      //listbox de catalogos
      app.directive('catalogSelect', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/catalog-select.html',
              controller: function ($scope, catalogService, $state) {
                  $scope.selectActionCatalog = function (cata) {
                      if (cata == 0) {
                          $scope.cata.selected = undefined;
                          return;
                      }
                      $scope.typeAction = 'module';
                      $scope.IdCatalog = cata.id;
                      $scope.NameCatalog = cata.Name;
                      if ($state.current.name == 'app.page.usercatalog') {
                          $scope.activeCheckbox = true;
                      }
                      if ($state.current.name != 'app.page.product_list') {
                          $scope.loadTreeCatalog(cata.id);
                      }
                  }

                  $scope.clear = function () {
                      $scope.catalog.selected = undefined;
                  };
                  $scope.loadCatalog = function () {
                      catalogService.getAll().then(function (response) {
                          $scope.cata = response.data;
                          $scope.catalogues = [$scope.cata.length];
                          angular.forEach($scope.cata, function (value, key) {
                              $scope.catalogues[key] = { id: value.CatalogId, Name: value.Name, Active: value.Active, Pending: value.Pending, Image: value.Image };
                          });
                      },
                      function (err) {
                          $scope.error_description = err.error_description;
                      });
                  }

                  $scope.loadCatalog();


              }
          };
      })
      app.directive('hotelSelect', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/hotel-select.html',
              controller: function ($scope, hotelService) {

                  $scope.clear = function () {
                      $scope.hotel.selected = undefined;
                  };

                  $scope.hotel = {};
                  $scope.selectActionHotel = function ()
                  {
                      hotelService.getHotelCatalogId($scope.hotel.selected.Id).then(function (response) {
                          var IdCatalog = response.data[0].IdCatalogue;
                          $scope.loadHotelTreeCatalog(IdCatalog);
                      });
                  }
                  hotelService.getAll().then(function (response) {
                      $scope.hotel = response.data;
                      $scope.hotels = [$scope.hotel.length]
                      angular.forEach($scope.hotel, function (value, key) {
                          $scope.hotels[key] = { Id: value.HotelId, Name: value.Name };
                      });
                  },
                  function (err) {
                      $scope.error_description = err.error_description;
                  });

              }
          };
      })
      app.directive('hotelCatalogTree', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/catalog-tree.html',
              controller: function ($scope, catalogService, $state) {
                  $scope.catalogtree = {};
                  
                

                  $scope.loadHotelTreeCatalog = function (catalogId) {
                      $scope.items = {};
                      $scope.sourceItems = {};
                      catalogService.getCatalogComplex(catalogId).then(function (response) {
                          $scope.catalogComplex = {};
                          $scope.catalogComplex.Modules = {};
                          $scope.sourceItems = JSON.parse(response.data);
                          $scope.items = $scope.sourceItems;                
                          //$scope.Modules = $scope.catalogComplex.Modules;
                          //angular.forEach($scope.Modules, function (valueModule, keyModule) {
                          //    if ($state.current.name == "app.page.usercatalog") {
                          //        $scope.sourceItems[keyModule].ActiveCheckbox = true;
                          //    }
                          //});
                      },
                      function (err) {
                          $scope.toaster = {
                              type: 'error',
                              title: 'Error',
                              text: err.error_description
                          };
                          $scope.pop();
                      });

                       
                   

                  }
                  $scope.loadHotelTreeCatalog($scope.IdCatalog);
              }
          }
      })
      app.directive('catalogTree', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/catalog-tree.html',
              controller: function ($scope, catalogService, $state) {
                  $scope.catalogtree = {};
                  $scope.sourceItems = [{}];                  
                  $scope.currentItem = {};

                  $scope.toggle = function (item) {
                      //$("input[name='post[]']").prop('checked', '')
                      for (var i = 0; i < $scope.products.length; i++)
                      {
                          $scope.products[i].checked = false;
                      }
                      if ($scope.currentItem) {
                          $scope.currentItem.selected = false;
                      }
                      item.selected = !item.selected;
                      $scope.currentItem = item;
                      
                      if ($state.current.name == 'app.page.catalogue_assignProducts') {
                          if ($scope.currentItem.type == 'module') {
                              $scope.toaster = {
                                  type: 'info',
                                  title: 'Info',
                                  text: 'The Module can not be selected'
                              };
                              $scope.pop();
                              $scope.currentItem.selected = false;
                          }
                          else {
                              if (!item.IsFinal)
                              {
                                  $scope.toaster = { type: 'info', title: 'Info', text: 'The Category cannot be selected' };
                                  $scope.pop();
                                  $scope.currentItem.selected = false;
                                  return;
                              }
                              $(this).addClass('selected').siblings().removeClass('selected');
                              $scope.getProductsByCategory();

                          }                          
                      }
                  };
                  $scope.modifyItems = function (item, obj)
                  {
                      $scope.IsNew = false;
                      if (item.type == "module") {
                          $scope.rootFileModule = '/images/catalogs/' + $scope.IdCatalog + '/modules/';
                          $scope.module = {
                              ModuleId: item.ModuleId,
                              IdTranslationName: item.IdTranslationName,
                              Prefix: item.Prefix,
                              Name: item.Name,
                              Image: item.Image,
                              Orden: item.Orden,
                              Comment: item.Comment,
                              Pending: item.Pending,
                              Active: item.Active,
                              Translation: {
                                  Id: item.Translation.Id,
                                  Spanish: item.Translation.Spanish,
                                  English: item.Translation.English,
                                  French: item.Translation.French,
                                  German: item.Translation.German,
                                  Language5: item.Translation.Language5,
                                  Language6: item.Translation.Language6,
                                  Language7: item.Translation.Language7,
                                  Language8: item.Translation.Language8,
                                  Active: item.Translation.Active
                              }
                          };
                          $scope.typeAction = 'module';
                          $scope.showTabsetCategory = false;
                          $scope.showTabsetModule = true;
                      } else {
                          $scope.rootFileCategory = '/images/catalogs/' + $scope.IdCatalog + '/categories/';
                          $scope.category = {
                              CategoryId: item.CategoryId,
                              IdTranslationName: item.IdTranslationName,
                              Prefix: item.Prefix,
                              Name: item.Name,
                              Image: item.Image,
                              Orden: item.Orden,
                              IsFirst: item.IsFirst,
                              IdParentCategory: item.IdParentCategory,
                              CategoryItem: item.CategoryItem,
                              Comment: item.Comment,
                              Pending: item.Pending,
                              IsFinal: item.IsFinal,
                              Active: item.Active,
                              Translation: {
                                  Id: item.Translation.Id,
                                  Spanish: item.Translation.Spanish,
                                  English: item.Translation.English,
                                  French: item.Translation.French,
                                  German: item.Translation.German,
                                  Language5: item.Translation.Language5,
                                  Language6: item.Translation.Language6,
                                  Language7: item.Translation.Language7,
                                  Language8: item.Translation.Language8,
                                  Active: item.Translation.Active

                              }
                          };
                          $scope.typeAction = 'category';
                          $scope.showTabsetCategory = true;
                          $scope.showTabsetModule = false;
                      }
                  }
                  $scope.addItems = function (item, obj) {
                      $scope.module = {};
                      $scope.category = {};
                      $scope.rootFileModule = '/img/';
                      $scope.rootFileCategory = '/img/';
                      $scope.module = { Image: 'no-image.jpg', Active: true };
                      $scope.category = { Image: 'no-image.jpg', Pending: true, IsFinal: true, Active: true };
                      if (item.type == "module") {
                          $scope.typeAction = 'module';
                          $scope.module = {
                              ModuleId : item.Id,
                              Name: item.Name,
                              Image: item.Image,
                              Orden: item.Orden,
                              Comment: item.Comment,
                              Pending: item.Pending,
                              Active: item.Active
                          };
                          $scope.typeAction = 'module';
                            $scope.category.IsFirst = true;
                            $scope.IdModule = item.Id;
                      }
                      else {
                          $scope.typeAction = 'category';
                          $scope.category.IsFirst = false;
                      }

                      $scope.currentModule = obj.item;
                      
                      while (obj.item.type != 'module') {
                          obj = obj.$parent;
                          $scope.currentModule = obj.item;                       
                      }

                      if (item.nextsibling == "category") {
                          if(item.type == 'category')
                              $scope.category.IdParentCategory = item.CategoryId;

                          $scope.typeAction = 'category';
                          $scope.showTabsetCategory = true;
                          $scope.showTabsetModule = false;

                      }
                      else {
                          $scope.showTabsetCategory = false;
                          $scope.showTabsetModule = true;
                      }

                      $scope.category = { Image: 'no-image.jpg', IsFirst: $scope.category.IsFirst, IdParentCategory: $scope.category.IdParentCategory, CategoryItem: $scope.categoryItem, Active: true, Pending: true, IsFinal: true };
                  }
                  $scope.deleteItems = function (item) {
                      if (item.type == "category") {
                          $scope.CategoryId = item.CategoryId;
                          $('#deleteCategory').modal({
                              show: 'true'
                          });
                      }
                      else {
                          $scope.ModulelId = item.ModuleId;
                          $('#deleteModule').modal({
                              show: 'true'
                          });

                      };
                      
                  }
                  $scope.removeModule = function (id)
                  {
                      catalogService.removeModule(id).then(function (response) {
                          $scope.toaster = {
                              type: 'success',
                              title: 'Info',
                              text: 'The Module has been removed'
                          };
                          $scope.pop();
                          $scope.loadTreeCatalog($scope.IdCatalog);
                      });
                  }
                  $scope.removeCategory = function (id) {
                      catalogService.removeCategory(id).then(function (response) {
                          $scope.toaster = {
                              type: 'success',
                              title: 'Info',
                              text: 'The Category has been removed'
                          };
                          $scope.pop();
                          $scope.loadTreeCatalog($scope.IdCatalog);
                      });
                  }

                  //Cargo la estructura compleja del catalogo
                  $scope.loadTreeCatalog = function (id)
                  {
                      if (!$scope.cata) {
                          $scope.items = {};
                          $scope.sourceItems = {};
                          return;
                      }
                      $scope.items = {};
                      $scope.sourceItems = {};
                      catalogService.getCatalogComplex(id).then(function (response) {
                          $scope.catalogComplex = {};
                          $scope.catalogComplex.Modules = {};
                          $scope.sourceItems = JSON.parse(response.data);
                          $scope.items = $scope.sourceItems;                
                      },
                      function (err) {
                            $scope.error_description = err.error_description;
                      });

                      $scope.items = $scope.sourceItems;    
                  }

                  $scope.loadTreeCatalog($scope.IdCatalog);
            
              }
          };
      })
      app.directive('userForm', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/user-form.html',
          };
      })
      app.directive('hotelForm', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/hotel-form.html'         
          };
      })
      app.directive('productForm', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/product-form.html'
          };
      })
      app.directive('productType', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/product-type.html',
              controller: function ($scope) {
                  $scope.productType = [{ name: 'Product' }, { name: 'Service' }, { name: 'Text' }];
              }
          };
      })

      app.directive('fileUpload', function () {
          return {
              restrict: 'E',
              templateUrl: 'tpl/partials/file-upload.html',
              controller: function ($scope, $http, $timeout, $upload) {
                      $scope.upload = [];
                      $scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2" };

                      $scope.onFileSelect = function ($files) {
                          //$files: an array of files selected, each file has name, size, and type.
                          for (var i = 0; i < $files.length; i++) {
                              var $file = $files[i];
                              (function (index) {
                                  $scope.upload[index] = $upload.upload({
                                      url: "./api/files/upload", // webapi url
                                      method: "POST",
                                      data: { fileUploadObj: $scope.fileUploadObj },
                                      file: $file
                                  }).progress(function (evt) {
                                      // get upload percentage
                                      console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                                  }).success(function (data, status, headers, config) {
                                      // file is uploaded successfully
                                      console.log(data);
                                  }).error(function (data, status, headers, config) {
                                      // file failed to upload
                                      console.log(data);
                                  });
                              })(i);
                          }
                      }

                      $scope.abortUpload = function (index) {
                          $scope.upload[index].abort();
                      }
              }
          };
      })

})();

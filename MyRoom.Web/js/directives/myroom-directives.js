﻿/// <reference path="../../tpl/partials/user-form.html" />
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
                      $scope.IdCatalog = cata.id;
                      $scope.NameCatalog = cata.Name;
                      if ($state.current.name == 'app.page.usercatalog') {
                          $scope.activeCheckbox = true;
                      }
                      $scope.loadTreeCatalog(cata.id);
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
                  
                  $scope.sourceItems = [
                        {
                            text: "Module A",
                            type: "module",
                            nextsibling: "category",
                            ActiveCheckbox: false,
                            children: [
                                {
                                    text: "Category A-1",
                                    type: "category",
                                    nextsibling: "category",
                                    ActiveCheckbox: true,
                                    children: [
                                        {
                                            text: "Category A-1-1",
                                            type: "category",
                                            nextsibling: "category",
                                            ActiveCheckbox: true,
                                            children: [
                                                { text: "Product B-3-1", type: "product", ActiveCheckbox: true },
                                                { text: "Product B-3-2", type: "product", ActiveCheckbox: true },
                                            ]
                                        }
                                    ]
                                }, {
                                    text: "Category A-2",
                                    type: "category",
                                    ActiveCheckbox: true
                                }, {
                                    text: "Category A-3",
                                    type: "category",
                                    ActiveCheckbox: true
                                },
                            ]
                        }   
      
                  ];

                  $scope.loadHotelTreeCatalog = function (id) {
                      //if (!$scope.cata) {
                      //    $scope.items = {};
                      //    $scope.sourceItems = {};
                      //    return;
                      //}
                      $scope.items = {};
                      $scope.sourceItems = {};
                      catalogService.getCatalogComplex(id).then(function (response) {
                          debugger
                          $scope.catalogComplex = {};
                          $scope.catalogComplex.Modules = {};
                          $scope.catalogComplex = response.data;
                          $scope.Modules = {};
                          /////////////////////////
                          $scope.Modules = $scope.catalogComplex.Modules;                          
                          angular.forEach($scope.Modules, function (valueModule, keyModule) {
                              $scope.sourceItems[keyModule] = {
                                  text: valueModule.Name,
                                  Prefix: valueModule.Prefix,
                                  type: "module",
                                  Id: valueModule.ModuleId,
                                  IdTranslationName: valueModule.IdTranslationName,
                                  Name: valueModule.Name,
                                  Image: valueModule.Image,
                                  Orden: valueModule.Orden,
                                  Comment: valueModule.Comment,
                                  Pending: valueModule.Pending,
                                  Active: valueModule.Active,
                                  nextsibling: "category",
                                  Translation: valueModule.Translation,
                              };

                              $scope.Category = {};
                              $scope.Category = $scope.Modules[keyModule].Categories;
                              $scope.sourceItems[keyModule].children = {};
                              angular.forEach($scope.Category, function (valueCategory, keyCategory) {
                                  $scope.sourceItems[keyModule].children[keyCategory] = {
                                      text: valueCategory.Name,
                                      type: "category",
                                      Id: valueCategory.CategoryId,
                                      IdTranslationName: valueCategory.IdTranslationName,
                                      Name: valueCategory.Name,
                                      Image: valueCategory.Image,
                                      Orden: valueCategory.Orden,
                                      Comment: valueCategory.Comment,
                                      Pending: valueCategory.Pending,
                                      IsFinal: valueCategory.IsFinal,
                                      Prefix: valueCategory.Prefix,
                                      nextsibling: "category",
                                      Translation: valueCategory.Translation

                                  };
                                  debugger
                                  $scope.sourceItems[keyModule].children[keyCategory] = createSubCategories($scope.Category[keyCategory].CategoryChildren, keyCategory, $scope.sourceItems[keyModule].children[keyCategory]);
                              });


                              if ($state.current.name == "app.page.usercatalog") {
                                  $scope.sourceItems[keyModule].ActiveCheckbox = true;
                              }
                          });
                          /////////////////////////////////////
                      },
                      function (err) {
                          $scope.error_description = err.error_description;
                      });

                      function createSubCategories(branch, keyCategory, sourceitems) {
                          if (branch == null)
                              return sourceitems;

                          sourceitems.children = {};
                          //angular.forEach(branch, function (valueCategory, keyCategory) {
                          sourceitems.children[keyCategory] = {
                              text: branch.Name,
                              type: "category",
                              Id: branch.CategoryId,
                              IdTranslationName: branch.IdTranslationName,
                              Name: branch.Name,
                              Image: branch.Image,
                              Orden: branch.Orden,
                              Comment: branch.Comment,
                              Pending: branch.Pending,
                              IsFirst: branch.IsFirst,
                              IsFinal: branch.IsFinal,
                              Active: branch.Active,
                              Prefix: branch.Prefix,
                              nextsibling: "category",
                              Translation: branch.Translation
                          };

                          //if (branch[keyCategory+1].CategoryChildren !== undefined) {
                          //    keyCategory++;
                          //}

                          debugger
                          if (branch.CategoryChildren == null)
                              return sourceitems;

                          return createSubCategories(branch.CategoryChildren, keyCategory, sourceitems.children[keyCategory]);


                      }
                      // $scope.catalog = cata;
                      $scope.items = $scope.sourceItems;


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
                      $("input[name='post[]']").prop('checked', '')
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
                              catalogService.getCategoryId(item.Id).then(function (response) {
                                  $scope.categoryProduct = response.data.value;
                                  angular.forEach($scope.categoryProduct, function (value, key) {
                                      angular.forEach($scope.products, function (valueProducts, keyProducts) {
                                          if (value.IdProduct == valueProducts.Id) {
                                              //Aca activamos el check
                                              $("input[name='post[]']").eq(keyProducts).prop('checked', 'checked')
                                          }
                                      });
                                  });
                                  

                              });
                          }
                          
                      }


                  };
                  $scope.modifyItems = function (item, obj)
                  {                     
                      $scope.IsNew = false;
                      if (item.type == "module") {
                         
                          $scope.module = {
                              ModuleId: item.Id,
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
                          },
                           
                          $scope.showTabsetCategory = false;
                          $scope.showTabsetModule = true;
                      } else {
                          $scope.category = {
                              CategoryId: item.Id,
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
                   
                          //$scope.category.categoryItem = obj.$parent.item.CategoryItem;
                          //$scope.category.IdParentCategory = obj.$parent.item.Id;

                          //$scope.category = { IsFirst: $scope.category.IsFirst, Active: true, Pending: true, IsFinal: true };
                          $scope.showTabsetCategory = true;
                          $scope.showTabsetModule = false;
                      }
                  }
                  $scope.addItems = function (item, obj) {
                      
                      if (item.type == "module") {
                          $scope.module = {
                              ModuleId : item.Id,
                              Name: item.Name,
                              Image: item.Image,
                              Orden: item.Orden,
                              Comment: item.Comment,
                              Pending: item.Pending,
                              Active: item.Active
                            };
                            $scope.category.IsFirst = true;
                            $scope.IdModule = item.Id;
                            $scope.CategoryItemId = null;
                      }
                      else {
                          $scope.category.IsFirst = false;
                          var _categoryItem = 0;
                          if (item.CategoryItem == null)
                          {
                              _categoryItem = item.Id;
                          } else {
                              _categoryItem = item.CategoryItem;
                          }
                          $scope.category.CategoryItem = _categoryItem;
                          $scope.CategoryItemId = _categoryItem;
                      }
                      while (obj.item.type != 'module') {
                          obj = obj.$parent;
                              $scope.currentModule = obj.item;
                       
                      }


                      var a = $scope.currentCategory;
                      if (item.nextsibling == "category") {
                          if(item.type == 'category')
                            $scope.category.IdParentCategory = item.Id;

                          $scope.showTabsetCategory = true;
                          $scope.showTabsetModule = false;

                      }
                      else {
                          $scope.showTabsetCategory = false;
                          $scope.showTabsetModule = true;
                      }
                      while (obj.item.type != 'module')
                      {
                          $scope.categoryItem = obj.item.Id;
                          obj = obj.$parent;                        
                      }
                      $scope.category = {IsFirst: $scope.category.IsFirst, IdParentCategory: $scope.category.IdParentCategory,  CategoryItem : $scope.categoryItem, Active: true, Pending: true, IsFinal: true };
                  }
                  $scope.deleteItems = function (item) {
                      if (item.type == "category") {
                          $scope.CategoryId = item.Id;
                          $('#deleteCategory').modal({
                              show: 'true'
                          });
                      }
                      else {
                          $scope.ModulelId = item.Id;
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
                         
                          $scope.catalogComplex = response.data;
                          $scope.Modules = {};
                          /////////////////////////
                          $scope.Modules = $scope.catalogComplex.Modules;
                          angular.forEach($scope.Modules, function (valueModule, keyModule) {
                                $scope.sourceItems[keyModule] = {
                                    text: valueModule.Name,
                                    Prefix: valueModule.Prefix,
                                    type: "module",
                                    Id: valueModule.ModuleId,
                                    IdTranslationName: valueModule.IdTranslationName,
                                    Name: valueModule.Name,
                                    Image: valueModule.Image,
                                    Orden: valueModule.Orden,
                                    Comment: valueModule.Comment,
                                    Pending: valueModule.Pending,
                                    Active: valueModule.Active,
                                    nextsibling: "category",
                                    Translation: valueModule.Translation,
                                };
                              
                                $scope.Category = {};
                                $scope.Category = $scope.Modules[keyModule].Categories;
                                $scope.sourceItems[keyModule].children = {};
                              //Categoria
                                angular.forEach($scope.Category, function (valueCategory, keyCategory) {
                                    
                                    $scope.sourceItems[keyModule].children[keyCategory] = {
                                        text: valueCategory.Name,
                                        type: "category",
                                        Id: valueCategory.CategoryId,
                                        IdTranslationName: valueCategory.IdTranslationName,
                                        Name: valueCategory.Name,
                                        Image: valueCategory.Image,
                                        IdParentCategory: valueCategory.IdParentCategory,
                                        CategoryItem: valueCategory.CategoryItem,
                                        Orden: valueCategory.Orden,
                                        Comment: valueCategory.Comment,
                                        Pending: valueCategory.Pending,
                                        IsFinal: valueCategory.IsFinal,
                                        Prefix: valueCategory.Prefix,
                                        nextsibling: "category",
                                        Translation: valueCategory.Translation

                                    };
                                    if ($state.current.name == 'app.page.catalogue_assignProducts') {
                                        if (valueCategory.IsFinal) {
                                            $scope.sourceItems[keyModule].children[keyCategory].ActiveCheckbox = true;
                                        }
                                    }
                                    if ($state.current.name == 'app.page.catalogue_assignProducts') {
                                        $scope.Product = {};
                                        $scope.sourceItems[keyModule].children[keyCategory].children = {};
                                        $scope.Product = $scope.Modules[keyModule].Categories[keyCategory].Products;
                                        angular.forEach($scope.Product, function (valueProduct, keyProduct) {
                                            console.info(valueProduct.Name);
                                            $scope.sourceItems[keyModule].children[keyCategory].children[keyProduct] = {
                                                text: valueProduct.Name,
                                                type: "product",
                                                Id: valueProduct.Id,
                                                IdTranslationName: valueProduct.IdTranslationName,
                                                Name: valueProduct.Name,
                                                Image: valueProduct.Image,
                                                Pending: valueProduct.Pending,
                                                IsFinal: valueProduct.IsFinal,
                                                Prefix: valueProduct.Prefix,
                                                nextsibling: "product",
                                                Translation: valueProduct.Translation
                                            };
                                            //$scope.sourceItems[keyModule].children[keyCategory].children[keyProduct] = createSubCategoriesProduct($scope.Product[keyProduct].CategoryChildren, keyProduct, $scope.sourceItems[keyModule].children[keyCategory].children[keyProduct]);
                                        });
                                    }
                                    debugger
                                    $scope.sourceItems[keyModule].children[keyCategory] = createSubCategories($scope.Category[keyCategory].CategoryChildren, keyCategory, $scope.sourceItems[keyModule].children[keyCategory]);

                                });
                                //Fin de la categoria

                                if ($state.current.name == "app.page.usercatalog") {
                                    $scope.sourceItems[keyModule].ActiveCheckbox = true;
                                }
                        });
                          /////////////////////////////////////
                      },
                      function (err) {
                            $scope.error_description = err.error_description;
                      });

                      function createSubCategories(branch, keyCategory, sourceitems) {
                          if (branch == null)
                              return sourceitems;

                          sourceitems.children = {};
                          //angular.forEach(branch, function (valueCategory, keyCategory) {
                          keyCategory = keyCategory + 1;
                          sourceitems.children[keyCategory] = {
                              text: branch.Name,
                              type: "category",
                              Id: branch.CategoryId,
                              IdTranslationName: branch.IdTranslationName,
                              Name: branch.Name,
                              Image: branch.Image,
                              Orden: branch.Orden,
                              Comment: branch.Comment,
                              IdParentCategory: branch.IdParentCategory,
                              CategoryItem: branch.CategoryItem,
                              Pending: branch.Pending,
                              IsFirst: branch.IsFirst,
                              IsFinal: branch.IsFinal,
                              Active: branch.Active,
                              Prefix: branch.Prefix,
                              nextsibling: "category",
                              Translation: branch.Translation
                          };
                          if ($state.current.name == 'app.page.catalogue_assignProducts') {
                              if (branch.IsFinal) {
                                  sourceitems.children[keyCategory].ActiveCheckbox = true;
                              }
                          }
                          
                          //$scope.Product = {};
                          //$scope.Product = sourceitems.children[keyCategory].Products;
                          if (branch.CategoryChildren == null)
                              return sourceitems;
                          console.info(keyCategory + branch.Name);
                          return createSubCategories(branch.CategoryChildren, keyCategory, sourceitems.children[keyCategory]);
                   

                      }
                      // $scope.catalog = cata;
                      //function createSubCategoriesProduct(branch, keyProducts, sourceitems) {
                      //    if (branch == null)
                      //        return sourceitems;

                      //    sourceitems.children = {};
                      //    //angular.forEach(branch, function (valueCategory, keyCategory) {
                      //    sourceitems.children[keyProducts] = {
                      //        text: branch.Name,
                      //        type: "product",
                      //        Id: branch.Id,
                      //        IdTranslationName: branch.IdTranslationName,
                      //        Name: branch.Name,
                      //        Image: branch.Image,
                      //        Orden: branch.Orden,
                      //        Comment: branch.Comment,
                      //        Pending: branch.Pending,
                      //        Active: branch.Active,
                      //        Prefix: branch.Prefix,
                      //        nextsibling: "product",
                      //        Translation: branch.Translation
                      //    };
                      //    if ($state.current.name == 'app.page.catalogue_assignProducts') {
                      //        if (branch.IsFinal) {
                      //            sourceitems.children[keyProduct].ActiveCheckbox = true;
                      //        }
                      //    }

                      //    $scope.Product = {};
                      //    $scope.Product = sourceitems.children[keyCategory].Products;
                      //    if (branch.CategoryChildren == null)
                      //        return sourceitems;

                      //    return createSubCategoriesProduct(branch.CategoryChildren, keyProduct, $scope.sourceItems[keyModule].children[keyCategory].children[keyProduct]);


                      //}
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

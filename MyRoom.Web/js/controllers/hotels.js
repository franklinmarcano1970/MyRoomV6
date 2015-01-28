﻿'use strict';

/* Controllers */
// Hotels controller
app.controller('HotelListController', ['$scope', '$http', '$state', 'hotelService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', function ($scope, $http, $state, hotelService, DTOptionsBuilder, DTColumnDefBuilder, toaster) {
    $scope.hotels = {};
    $scope.currentHotelId = 0;;
    $scope.toaster = {
        type: 'success',
        title: 'Info',
        text: 'The hotel has been removed'
    };
    angular.element(document).ready(function () {
        $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');
        $scope.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(0),
            DTColumnDefBuilder.newColumnDef(1),
            DTColumnDefBuilder.newColumnDef(2),
            DTColumnDefBuilder.newColumnDef(3).notSortable(),
            DTColumnDefBuilder.newColumnDef(4).notSortable()
        ];
        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };
        //Button Create
        $scope.createHotel = function () {
            $state.go('app.page.hotel_create');
        }
        $scope.getAll = function () {
            hotelService.getAll().then(function (response) {
                $scope.hotels = response.data;

            },
            function (err) {
                $scope.error_description = err.error_description;
            });
        };

        $scope.selectHotel = function (id) {
            $scope.currentHotelId = id;
            $('#deleteHotel').modal({
                show: 'true'
            });
        }

        $scope.modifyHotel = function (id) {
            $scope.currentHotelId = id;
            $state.go('app.page.hotel_edit', { "id": id });
        }

        $scope.removeHotel = function (id) {
            hotelService.removeHotel(id).then(function (response) {
                $scope.hotel = {
                    Active: true,
                    Image: 'img/hotel.jpg',
                    Translation: {
                        Active: true
                    }
                };
                $scope.pop();
                $scope.getAll();
            },
            function (err) {
                $scope.error_description = err.error_description;
            });
        };

        $scope.getAll();
    });
}]);
app.controller('HotelsController', ['$scope', '$http', '$state', 'hotelService', 'toaster', '$timeout', 'FileUploader', function ($scope, $http, $state, hotelService, toaster, $timeout, FileUploader) {
    //$scope.file = {file: 1, Name: "ff.jpg"};
    var uploader = $scope.uploader = new FileUploader({
        url: serviceBase + 'api/files/Upload' 
    });
    $scope.hotel = {
        Name: '',
        Active: true,
        Image: 'img/no-image.jpg',
        Translation: {
            Spanish: '',
            English: '',
            French: '',
            German: '',
            Language5: '',
            Language6: '',
            Language7: '',
            Language8: '',
            Active: true
        },
    };
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
        $scope.hotel.Image = response[0].path;
    };
    uploader.onAfterAddingFile = function (fileItem) {
        if (fileItem.file.type == 'image/jpeg') {
            $scope.file = fileItem._file;
            $scope.hotel.Image = $scope.file.name;
            var fr = new FileReader();
            fr.onload = function (e) {
                $('#image')
                    .attr('src', e.target.result)
                //.width(150)
                //.height(200);
            }
            fr.readAsDataURL(fileItem._file);
        }
        else {
            $scope.toaster = {
                type: 'error',
                title: 'Info',
                text: 'File permited JPEG or GIF'
            };
            $scope.pop();
        }
    };
    if ($state.current.name == "app.page.hotel_edit" && $state.params['id']) {
        hotelService.getHotel($state.params['id']).then(function (response) {
            $scope.hotel = JSON.parse(response.data);;
        });
    };

    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };
    var apple_selected, tree, treedata_avm, treedata_geography;
    $scope.IdTranslations = 0;
  //  $scope.Mensaje = "";
    $scope.NameCatalogue = "";
    //$scope.hotels = [];
    //$scope.catalogues = [];

    
    $scope.IsNewRecord = 1;
    $scope.my_tree = tree = {};
    $scope.my_data = [{ }]
    treedata_avm = [];
    $scope.saveHotel = function () {
        var file = $scope.uploader;
        
        if ($state.current.name == "app.page.hotel_create") {
            hotelService.saveHotel($scope.hotel).then(function (response) {
                $scope.hotel = {
                    Active: true,
                    Image: 'img/hotel.jpg',
                    Translation: {
                        Active: true
                    }
                };
                uploader.uploadAll();
                $timeout(function () {
                    $scope.toaster = {
                        type: 'success',
                        title: 'Success',
                        text: 'The Hotel has been saved'
                    };

                    $scope.pop();
                }, 1000);
                $state.go('app.page.hotel_list');
              //  $scope.message = "The Hotel has been saved";
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
        else {
          //  delete $scope.hotel.$id;
           // delete $scope.hotel.Translation.$id
            hotelService.updateHotel($scope.hotel).then(function (response) {
                $scope.toaster = {
                    type: 'success',
                    title: 'Success',
                    text: 'The Hotel has been updated'
                };

                $timeout(function () {
                    $scope.pop();
                    
                }, 1000).then(function () {
                });
                $state.go('app.page.hotel_list');
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
    };

    function getChildren(id) {
        var getCategory = $http.get(serviceBase + "odata/RelModuleCategory(" + id + ")/Categories");
        var items = [];
        getCategory.then(function (pl) {
            var myObj = pl.data;
            var RelModuleCategory = [];

            RelModuleCategory = myObj.value;
            var i = 0;
            var j = $scope.sourceItems.length + 1;
            for (i; i < RelModuleCategory.length; i++) {
                //$scope.Catalog[j] = { "Name": RelModuleCategory[i].Name };
                items[i] = {
                    text: RelModuleCategory[i].Name,
                    type: "category",
                    nextsibling: "category",
                    Id: RelModuleCategory[i].Id,
                    children: getChildrenProduct(RelModuleCategory[i].Id)
                };
                j++;
            };
        },
        function (errorPl) {
            $log.error('failure loading users', errorPl);
        });

        return items;
    }

    function getChildrenProduct(id) {
        //RelCategoryProducts(3) / Products
        var getCategory = $http.get(serviceBase + "odata/RelCategoryProducts(" + id + ")/Products");
        var items = [];
        getCategory.then(function (pl) {
            var myObj = pl.data;
            var RelModuleCategory = [];

            RelModuleCategory = myObj.value;
            var i = 0;
            var j = $scope.sourceItems.length + 1;
            for (i; i < RelModuleCategory.length; i++) {
                items[i] = {
                    text: RelModuleCategory[i].Name,
                    type: "product",
                    nextsibling: "category",
                    Id: RelModuleCategory[i].Id
                };
                j++;
            };
        },
        function (errorPl) {
            $log.error('failure loading products category', errorPl);
        });

        return items;
    }
    //Buscar el catalogo activo para ese hotel en ACTIVE_HOTEL_CATALOGUE
    $scope.selectActionCatalogHotel = function (id) {
        $scope.IdHotel = id;
        var getActiveHotelCatalogue = $http.get(serviceBase + "odata/Hotels(" + id + ")/ActiveHotelCatalogue?filter=Active eq true");
        getActiveHotelCatalogue.then(function (pl) {
            var myObj = pl.data;
            $scope.ActiveHotelCatalogue = myObj.value;
            $scope.IdCatalog = $scope.ActiveHotelCatalogue[0].IdCatalogue
            $scope.NameCatalogue = $scope.ActiveHotelCatalogue[0].IdCatalogue;
            var GetCatalogue = $http.get(serviceBase + "odata/Catalogues(" + $scope.IdCatalog + ")");
            GetCatalogue.then(function(pl)
            {
                var myObj = pl.data;
                $scope.NameCatalogue = myObj.Name;

                //Listar la estrucutura del arbol con la data de los modulos, las categorias, subcategorias y los productos del catalogo activo del hotel
                var CatalogueCategory = $http.get(serviceBase + "odata/RelCatalogueModules(" + $scope.IdCatalog + ")/Modules");
                CatalogueCategory.then(function (pl) {
                    var myObj = pl.data;
                    $scope.CatalogueCategory = myObj.value;

                    var i = 0;
                    for (i; i < $scope.CatalogueCategory.length; i++) {
                        $scope.sourceItems[i] = {
                            text: $scope.CatalogueCategory[i].Name,
                            type: "module",
                            nextsibling: "category",
                            Id: $scope.CatalogueCategory[i].Id,
                            children: getChildren($scope.CatalogueCategory[i].Id)
                        }

                    }
                    console.info(myObj);
                },
                function (errorPL) {
                })

            },
            function(errorPL){
            })
           
        },
        function (errorPl) {
            $log.error('failure loading Active hotel catalogue', errorPl);
        });
    }



    //Guardar los registros marcados en el check box y guardar registros en las siguientes tablas: ACTIVE_HOTEL_MODULE, ACTIVE_HOTEL_CATEGORY, ACTIVE_HOTEL_PRODUCT
    $scope.activeProduct = function ()
    {
        var i = 0;
        for (i; i < $scope.sourceItems.length; i++)
        {
            var j = 0;
            for (j; j < $scope.sourceItems[i].children.length; j++) {
                var k = 0;
                for (k; k < $scope.sourceItems[i].children[j].children.length; k++) {
                    if ($scope.sourceItems[i].children[j].children[k]._Selected == true) {

                        //Procedemos a grabar en ACTIVE_HOTEL_PRODUCT

                        var ActiveHotelProduct = {
                            IdHotel: $scope.IdHotel,
                            IdProduct: $scope.sourceItems[i].children[j].children[k].Id,
                            Active: true
                        };
                        var request = $http({
                            method: "post",
                            url: serviceBase + "odata/ActiveHotelProduct",
                            data: ActiveHotelProduct
                        });

                        var post = request;
                        post.then(function (pl) {

                        }, function (err) {
                            console.log("Err" + err);
                        });
                        console.info($scope.sourceItems[i].children[j].children[k].Id + " Activado");
                    }
                }
            }
        }
        

    }
    
    $scope.activeCheckbox = function ()
    {
        return item.type == 'product' || item.type == 'category';
    }
}])
;
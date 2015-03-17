'use strict';

/* Controllers */
// Hotels controller
app.controller('HotelListController', ['$scope', '$http', '$state', 'hotelService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', function ($scope, $http, $state, hotelService, DTOptionsBuilder, DTColumnDefBuilder, toaster) {
    $scope.hotels = {};
    $scope.currentHotelId = 0;;

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
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();

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
                $scope.toaster = {
                    type: 'success',
                    title: 'Info',
                    text: 'The hotel has been removed'
                };
                $scope.hotel = {
                    Active: true,
                    UTC: '',
                    ChangeSummerTime: false,
                    ContentIframeSurvey: '',
                    Image: 'img/hotel.jpg',
                    UrlScanMap: 'img/hotel.jpg',
                    Translation: {
                        Active: true
                    }
                };
                $scope.pop();
                $scope.getAll();
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();

            });
        };

        $scope.getAll();
    });
}]);
app.controller('HotelsController', ['$scope', '$http', '$state', 'hotelService', 'ngWebBaseSettings', 'toaster', '$timeout', 'FileUploader', function ($scope, $http, $state, hotelService, ngWebBaseSettings, toaster, $timeout, FileUploader) {

    var uploader = $scope.uploader = new FileUploader({
        url: ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=1-0-0'
    });
    var uploaderUrl = $scope.uploaderUrl = new FileUploader({
        url: ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=1-0-0'
    });

    $scope.hotel = {
        Name: '',
        UTC: '',
        ChangeSummerTime: false,
        ContentIframeSurvey: '',
        Active: true,
        Image: '/img/no-image.jpg',
        UrlScanMap: '/img/no-image.jpg',

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
        $state.go('app.page.hotel_list');
    };
    uploaderUrl.onSuccessItem = function (fileItem, response, status, headers) {
        $state.go('app.page.hotel_list');
    };
    uploader.onAfterAddingFile = function (fileItem) {
        if (fileItem.file.size > ngWebBaseSettings.fileSize) {
            $scope.toaster = {
                type: 'error',
                title: 'Info',
                text: 'File too big'
            };
            $scope.pop();
            return;
        }
        $scope.file = fileItem._file;
        $scope.hotel.Image = "/" + ngWebBaseSettings.rootFileHotel +  $scope.file.name;
        var fr = new FileReader();
        fr.onload = function (e) {
            $('#image')
                .attr('src', e.target.result)
        }
        fr.readAsDataURL(fileItem._file);
    };
    uploaderUrl.onAfterAddingFile = function (fileItem) {
        if (fileItem.file.size > ngWebBaseSettings.fileSize) {
            $scope.toaster = {
                type: 'error',
                title: 'Info',
                text: 'File too big'
            };
            $scope.pop();
            return;
        }
        $scope.file = fileItem._file;
        $scope.hotel.UrlScanMap = "/" + ngWebBaseSettings.rootFileHotel + $scope.file.name;
        var fr = new FileReader();
        fr.onload = function (e) {
            $('#UrlScanMapImage')
                .attr('src', e.target.result)
        }
        fr.readAsDataURL(fileItem._file);
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
                    UTC: '',
                    ChangeSummerTime: false,
                    ContentIframeSurvey: '',
                    Image: 'img/hotel.jpg',
                    UrlScanMap: 'img/hotel.jpg',
                    Translation: {
                        Active: true
                    }
                };
                uploader.uploadAll();
                uploaderUrl.uploadAll();
                $timeout(function () {
                    $scope.toaster = {
                        type: 'success',
                        title: 'Success',
                        text: 'The Hotel has been saved'
                    };
                    //$state.go('app.page.hotel_list');
                    $scope.pop();
                }, 2000);
            },
            function (err) {
                $scope.toaster = {type: 'error',title: 'Error',text: err.error_description};
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
                uploader.uploadAll();
                uploaderUrl.uploadAll();
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

    //function getChildren(id) {
    //    var getCategory = $http.get(serviceBase + "odata/RelModuleCategory(" + id + ")/Categories");
    //    var items = [];
    //    getCategory.then(function (pl) {
    //        var myObj = pl.data;
    //        var RelModuleCategory = [];

    //        RelModuleCategory = myObj.value;
    //        var i = 0;
    //        var j = $scope.sourceItems.length + 1;
    //        for (i; i < RelModuleCategory.length; i++) {
    //            //$scope.Catalog[j] = { "Name": RelModuleCategory[i].Name };
    //            items[i] = {
    //                text: RelModuleCategory[i].Name,
    //                type: "category",
    //                nextsibling: "category",
    //                Id: RelModuleCategory[i].Id,
    //                children: getChildrenProduct(RelModuleCategory[i].Id)
    //            };
    //            j++;
    //        };
    //    },
    //    function (errorPl) {
    //        $log.error('failure loading users', errorPl);
    //    });

    //    return items;
    //}

    //function getChildrenProduct(id) {
    //    //RelCategoryProducts(3) / Products
    //    var getCategory = $http.get(serviceBase + "odata/RelCategoryProducts(" + id + ")/Products");
    //    var items = [];
    //    getCategory.then(function (pl) {
    //        var myObj = pl.data;
    //        var RelModuleCategory = [];

    //        RelModuleCategory = myObj.value;
    //        var i = 0;
    //        var j = $scope.sourceItems.length + 1;
    //        for (i; i < RelModuleCategory.length; i++) {
    //            items[i] = {
    //                text: RelModuleCategory[i].Name,
    //                type: "product",
    //                nextsibling: "category",
    //                Id: RelModuleCategory[i].Id
    //            };
    //            j++;
    //        };
    //    },
    //    function (errorPl) {
    //        $log.error('failure loading products category', errorPl);
    //    });

    //    return items;
    //}
    //Buscar el catalogo activo para ese hotel en ACTIVE_HOTEL_CATALOGUE
    //$scope.selectActionCatalogHotel = function (id) {
    //    $scope.IdHotel = id;
    //    var getActiveHotelCatalogue = $http.get(serviceBase + "odata/Hotels(" + id + ")/ActiveHotelCatalogue?filter=Active eq true");
    //    getActiveHotelCatalogue.then(function (pl) {
    //        var myObj = pl.data;
    //        $scope.ActiveHotelCatalogue = myObj.value;
    //        $scope.IdCatalog = $scope.ActiveHotelCatalogue[0].IdCatalogue
    //        $scope.NameCatalogue = $scope.ActiveHotelCatalogue[0].IdCatalogue;
    //        var GetCatalogue = $http.get(serviceBase + "odata/Catalogues(" + $scope.IdCatalog + ")");
    //        GetCatalogue.then(function(pl)
    //        {
    //            var myObj = pl.data;
    //            $scope.NameCatalogue = myObj.Name;

    //            //Listar la estrucutura del arbol con la data de los modulos, las categorias, subcategorias y los productos del catalogo activo del hotel
    //            var CatalogueCategory = $http.get(serviceBase + "odata/RelCatalogueModules(" + $scope.IdCatalog + ")/Modules");
    //            CatalogueCategory.then(function (pl) {
    //                var myObj = pl.data;
    //                $scope.CatalogueCategory = myObj.value;

    //                var i = 0;
    //                for (i; i < $scope.CatalogueCategory.length; i++) {
    //                    $scope.sourceItems[i] = {
    //                        text: $scope.CatalogueCategory[i].Name,
    //                        type: "module",
    //                        nextsibling: "category",
    //                        Id: $scope.CatalogueCategory[i].Id,
    //                        children: getChildren($scope.CatalogueCategory[i].Id)
    //                    }

    //                }
    //                console.info(myObj);
    //            },
    //            function (errorPL) {
    //            })

    //        },
    //        function(errorPL){
    //        })
           
    //    },
    //    function (errorPl) {
    //        $log.error('failure loading Active hotel catalogue', errorPl);
    //    });
    //}



    ////Guardar los registros marcados en el check box y guardar registros en las siguientes tablas: ACTIVE_HOTEL_MODULE, ACTIVE_HOTEL_CATEGORY, ACTIVE_HOTEL_PRODUCT
    //$scope.activeProduct = function ()
    //{
    //    var i = 0;
    //    for (i; i < $scope.sourceItems.length; i++)
    //    {
    //        var j = 0;
    //        for (j; j < $scope.sourceItems[i].children.length; j++) {
    //            var k = 0;
    //            for (k; k < $scope.sourceItems[i].children[j].children.length; k++) {
    //                if ($scope.sourceItems[i].children[j].children[k]._Selected == true) {

    //                    //Procedemos a grabar en ACTIVE_HOTEL_PRODUCT

    //                    var ActiveHotelProduct = {
    //                        IdHotel: $scope.IdHotel,
    //                        IdProduct: $scope.sourceItems[i].children[j].children[k].Id,
    //                        Active: true
    //                    };
    //                    var request = $http({
    //                        method: "post",
    //                        url: serviceBase + "odata/ActiveHotelProduct",
    //                        data: ActiveHotelProduct
    //                    });

    //                    var post = request;
    //                    post.then(function (pl) {

    //                    }, function (err) {
    //                        console.log("Err" + err);
    //                    });
    //                    console.info($scope.sourceItems[i].children[j].children[k].Id + " Activado");
    //                }
    //            }
    //        }
    //    }
        

    //}

    $scope.activeCheckbox = function ()
    {
        return item.type == 'product' || item.type == 'category';
    }
}])
app.controller('HotelsAssingProductController', ['$scope', '$http', '$state', 'hotelService', 'ngWebBaseSettings', 'toaster', '$timeout', '$filter', function ($scope, $http, $state, hotelService, ngWebBaseSettings, toaster, $timeout, $filter) {
    var GetCheckedTreeNode = $filter('GetCheckedTreeNode');
    //Guardar los registros marcados en el check box y guardar registros en las siguientes tablas: ACTIVE_HOTEL_MODULE, ACTIVE_HOTEL_CATEGORY, ACTIVE_HOTEL_PRODUCT
    

    $scope.showHotelAssignPopup = function () {
        if ($scope.hotel.selected) {
            $('#popupHotelAssignProduct').modal({
                show: 'true'
            });
        }
        else {
            $scope.toaster = { type: 'success', title: 'Success', text: 'Please select a hotel' };
            $scope.pop();
        }
    }
    $scope.activeProduct = function () {
        var getChecked = GetCheckedTreeNode($scope.items);
        var vm = {
            HotelId: $scope.hotel.selected.Id,
            'HotelCatalog': []
        };
        angular.forEach(getChecked, function (item) {
            vm.HotelCatalog.push({  ElementId: item.id, Type: item.type });
        });
        hotelService.saveActiveProduct(vm).then(function (response) {
            $scope.toaster = { type: 'success', title: 'Success', text: 'The assign elements has been to hotel' };
            $scope.pop();
        },
        function (err) {
            $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
            $scope.pop();
        });
    }
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };
    $scope.activeCheckbox = function () {
        return item.type == 'product' || item.type == 'category';
    }
}]);
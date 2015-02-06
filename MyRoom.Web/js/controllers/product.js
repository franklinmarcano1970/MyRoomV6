'use strict';
/* Controllers */
// product controller
app.controller('ProductsController', ['$scope', '$http', '$state', 'productService', 'DTOptionsBuilder', 'DTColumnDefBuilder', '$filter', 'toaster', '$timeout', 'FileUploader', 'ngWebBaseSettings', function ($scope, $http, $state, productService, DTOptionsBuilder, DTColumnDefBuilder, $filter, toaster, $timeout, FileUploader, ngWebBaseSettings) {
    var uploader = $scope.uploader = new FileUploader({
        //url: ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=5-0-0'
    });
    var ischecked = $filter('ischecked');
    $scope.IdCatalog = 0;
    $scope.toaster = {
        type: 'error',
        title: 'Info',
        text: 'Only 6 Related product are permited'
    };
    $scope.products = {};
    $scope.rootFile = '/img/';
    $scope.product = {
        Prefix: '',
        Name: '',
        Description: '',
        Price: '',
        Active: true,
        Image: 'no-image.jpg',
        Order: '',
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
        RelatedProducts: [{ IdRelatedProduct: 0 }]
    };
    $scope.menssage = '';
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
        $state.go('app.page.product_list');
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
        $scope.fileItem = fileItem;
        $scope.product.Image = $scope.file.name;
        var fr = new FileReader();
        fr.onload = function (e) {
            $('#imageProd')
                .attr('src', e.target.result)
        }
        fr.readAsDataURL(fileItem._file);
    };
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };

    if ($state.current.name == "app.page.product_edit" && $state.params['id']) {
        var param = $state.params['id'].split("-");
        var id = param[0];
        var idCatalog = param[1];
        $scope.IdCatalog = idCatalog;
        productService.getProduct(id).then(function (response) {
            $scope.product = JSON.parse(response.data);
            $scope.rootFile = '/images/' + $scope.IdCatalog + '/products/';
            productService.getAll().then(function (response) {
                $scope.products = response.data.filter(function (e) {
                   return e.Id !== $scope.product.Id;
                });

                for (var j = 0 ; j < $scope.product.RelatedProducts.length; j++) {
                    for (var i = 0 ; i < $scope.products.length; i++) {
                        if ($scope.products[i].Id == $scope.product.RelatedProducts[j].IdRelatedProduct) {
                            $scope.products[i].checked = true;
                        }
                    }
                }

            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $timeout(function () {
                    $scope.pop();
                }, 1000);
            });

            for (var i = 0; i < $scope.products.length - 1; i++) {
                if ($scope.products[i].Id == $scope.product.RelatedProducts[i].IdRelatedProduct)
                    response.data[i].checked = true;
            }
        });


    }
    else {
        productService.getAll().then(function (response) {
            $scope.products = response.data;
        },
          function (err) {
              $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
              $timeout(function () {
                  $scope.pop();
              }, 1000);
          });

    }

    function createProductVM(entity) {
        $scope.product.RelatedProducts = [];
       
        angular.forEach($scope.products, function (value, key) {
            if (value.checked == true) {
                $scope.product.RelatedProducts.push({ IdProduct: $scope.product.Id, IdRelatedProduct: value.Id });

            }
        });
        var vm = {};
        vm.Name = entity.Name;
        vm.Image = entity.Image;
        vm.Description = entity.Description;
        vm.Price = entity.Price;
        vm.ProductActive = entity.ProductActive;
        vm.Prefix = entity.Prefix;        
        vm.Order = entity.Order;
        vm.Type = entity.Type;
        vm.Name_ENG = entity.Name_ENG;
        vm.Description_ENG = entity.Description_ENG;
        vm.UrlScanDocument = entity.UrlScanDocument;
        vm.Pending = entity.Pending;
        vm.Active = entity.Active
        vm.Spanish = entity.Translation.Spanish;
        vm.English = entity.Translation.English;
        vm.French = entity.Translation.French;
        vm.German = entity.Translation.German;
        vm.TranslationActive = entity.Translation.Active;

        vm.Language5 = entity.Translation.Language5;
        vm.Language6 = entity.Translation.Language6;
        vm.Language7 = entity.Translation.Language7;
        vm.Language8 = entity.Translation.Language8;


        vm.RelatedProducts = $scope.product.RelatedProducts;
        
        return vm;
    }

    $scope.saveProduct = function () {
        
        var productVm = createProductVM($scope.product);
        if ($state.current.name == "app.page.product_create" && $state.params['id']) {
            $scope.IdCatalog = $state.params['id'];
            $scope.rootFile = '/images/' + $scope.IdCatalog + '/';
     
            productService.saveProduct(productVm).then(function (response) {
                //$scope.Id = response.data.Id;
                $scope.toaster = { type: 'success', title: 'Info', text: 'The Product has been saved' };
                $timeout(function () {
                    $scope.pop();
                }, 1000);
                //Para subir la imagen
                $scope.fileItem.url = ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=5-' + $scope.IdCatalog + '-0';
                uploader.uploadAll();
                $scope.product = {
                    Active: true,
                    Image: 'no-image.jpg',
                    Translation: {
                        Active: true
                    },
                    RelatedProducts: []
                };
                //$state.go('app.page.product_list');
                // $scope.message = "The Product has been saved";
            },
            function (err) {
                $scope.toaster = {
                    type: 'error',
                    title: 'Error',
                    text: err.error_description
                };
            });
        }
        else {
            productService.updateProduct($scope.product).then(function (response) {
                $scope.toaster = { type: 'success', title: 'Info', text: 'The Product has been updated' };
                $scope.fileItem.url = ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=5-' + $scope.IdCatalog + '-0';
                uploader.uploadAll();
                $timeout(function () {
                    $scope.pop();
                }, 1000);
                $scope.product = {
                    Active: true,
                    Image: 'no-image.jpg',
                    Translation: {
                        Active: true
                    },
                    RelatedProducts: []
                };
                $state.go('app.page.product_list');
            },
            function (err) {
                $scope.toaster = {
                    type: 'error',
                    title: 'Error',
                    text: err.error_description
                };
            });
        }
      


    };
   

//    $scope.getAllProduct();
}]);

app.controller('ProductsListController', ['$scope', '$http', '$state', 'productService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', '$timeout', function ($scope, $http, $state, productService, DTOptionsBuilder, DTColumnDefBuilder, toaster, $timeout) {
    $scope.products = {};
    $scope.currentProdId = 0;
    $scope.IdCatalog = 0;
    $scope.toaster = {
        type: 'success',
        title: 'Info',
        text: 'The product has been removed'
    };

    angular.element(document).ready(function () {
        $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');
        $scope.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef('Id'),
            DTColumnDefBuilder.newColumnDef('Name'),
            DTColumnDefBuilder.newColumnDef('Price'),
            DTColumnDefBuilder.newColumnDef('Prefix'),
            DTColumnDefBuilder.newColumnDef('Type'),
            DTColumnDefBuilder.newColumnDef('Active').notSortable(),
            DTColumnDefBuilder.newColumnDef(null).notSortable(),

        ];
        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };
        $scope.createProduct = function () {
            if ($scope.IdCatalog == 0) {
                $scope.toaster = {
                    type: 'error',
                    title: 'Error',
                    text: 'Selected Catalog'
                };
                $scope.pop();
                return;
            }
            $state.go('app.page.product_create', { "id": $scope.IdCatalog });
        };

        $scope.modifyProduct = function (id) {
            if ($scope.IdCatalog == 0) {
                $scope.toaster = {
                    type: 'error',
                    title: 'Error',
                    text: 'Selected Catalog'
                };
                $scope.pop();
                return;
            }
            $scope.currentProdId = id;
            $state.go('app.page.product_edit', { "id": id + "-" + $scope.IdCatalog });
        }

        $scope.getAll = function () {
            productService.getAll().then(function (response) {
                $scope.products = response.data;
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $timeout(function () {
                    $scope.pop();
                }, 1000);
            });
        };

       


        $scope.selectProduct = function (id) {
            $scope.currentProdId = id;
            $('#deleteProduct').modal({
                show: 'true'
            });
        }

        $scope.removeProduct = function (id) {
            productService.removeProduct(id).then(function (response) {
                //$scope.Id = response.data.Id;
                $scope.product = {
                    Active: true,
                    Image: 'no-image.jpg',
                    Translation: {
                        Active: true
                    }
                };
                $scope.pop();
                $scope.getAll();
                $scope.message = "The Product has been removed";
            },
            function (err) {
                $scope.error_description = err.error_description;
            });

            
        };
        
        $scope.getAll();
    });
}])
;
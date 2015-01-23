'use strict';
/* Controllers */
// product controller
app.controller('ProductsController', ['$scope', '$http', '$state', 'productService', 'DTOptionsBuilder', 'DTColumnDefBuilder', '$filter', 'toaster', '$timeout', function ($scope, $http, $state, productService, DTOptionsBuilder, DTColumnDefBuilder, $filter, toaster, $timeout) {
    var ischecked = $filter('ischecked');
    $scope.toaster = {
        type: 'error',
        title: 'Info',
        text: 'Only 6 Related product are permited'
    };
    $scope.products = {};
    $scope.product = {
        Prefix: '',
        Name: '',
        Description: '',
        Price: '',
        Active: true,
        Image: 'img/prod.jpg',
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
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };

    if ($state.current.name == "app.page.product_edit" && $state.params['id']) {
        productService.getProduct($state.params['id']).then(function (response) {
            $scope.product = JSON.parse(response.data);
            productService.getAll().then(function (response) {
                //for (var i = 0; i < response.data.length; i++) {
                //    if ($scope.product.Id == response.data[i].Id) {
                //        //remove equal product
                        
                //        //response.data = response.data.splice(response.data[i].Id, 1);
                //    }
                //}

          


                $scope.products = response.data;
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

    $scope.saveProduct = function () {
        $scope.product.RelatedProducts = [];
        //var i = 0;
        angular.forEach($scope.products, function (value, key) {
            debugger
            if (value.checked == true) {
                $scope.product.RelatedProducts.push({ IdRelatedProduct: value.Id });

                //$scope.product.RelatedProducts[i] = { IdRelatedProduct: value.Id };

            }
        });

        if ($state.current.name == "app.page.product_create") {
            productService.saveProduct($scope.product).then(function (response) {
                //$scope.Id = response.data.Id;
                $scope.toaster = { type: 'success', title: 'Info', text: 'The Product has been saved' };
                $timeout(function () {
                    $scope.pop();
                }, 1000);

                $state.go('app.page.product_list');
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
                $timeout(function () {
                    $scope.pop();
                }, 1000);

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
        $scope.product = {
            Active: true,
            Image: 'img/prod.jpg',
            Translation: {
                Active: true
            },
            RelatedProducts: []
        };


    };
   

//    $scope.getAllProduct();
}]);

app.controller('ProductsListController', ['$scope', '$http', '$state', 'productService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', '$timeout', function ($scope, $http, $state, productService, DTOptionsBuilder, DTColumnDefBuilder, toaster, $timeout) {
    $scope.products = {};
    $scope.currentProdId = 0;
    $scope.toaster = {
        type: 'success',
        title: 'Info',
        text: 'The product has been removed'
    };

    angular.element(document).ready(function () {
        $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');
        $scope.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(0),
            DTColumnDefBuilder.newColumnDef(1).order,
            DTColumnDefBuilder.newColumnDef(2),
            DTColumnDefBuilder.newColumnDef(3),
            DTColumnDefBuilder.newColumnDef(4),
            DTColumnDefBuilder.newColumnDef(5).notSortable(),
            DTColumnDefBuilder.newColumnDef(6).notSortable(),

        ];
        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };
        $scope.createProduct = function () {
            $state.go('app.page.product_create');
        };

        $scope.modifyProduct = function (id) {
            $scope.currentProdId = id;
            $state.go('app.page.product_edit', { "id": id });
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
                    Image: 'img/prod.jpg',
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
'use strict';

/* Controllers */
// Catalogues controller
app.controller('CataloguesController', ['$scope', '$http', '$state', 'catalogService', 'toaster', function ($scope, $http, $state, catalogService, toaster) {
    var IdCatalog = 0;
    $scope.IdModule = 0
    $scope.IdCatalog = 0;
    $scope.NameCatalog = '';
    $scope.IdCategory= 0;
    $scope.IdTranslations = 0;
    $scope.Mensaje = "";
    $scope.catalogues = [];
    $scope.activeCheckbox = false;
    $scope.IsNew = true;
    $scope.catalog = {
        Name: '',
        Image: 'noimage.jpg',
        Pending: false,
        Active: false,
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
        }
    };

    $scope.module = {
        ModuleId: 0,
        Name: '',
        Image: 'noimage.jpg',
        Orden: '',
        Comment: '',
        Pending: false,
        Active: false,
        Prefix: '',
        Translation: {
            Id: 0,
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
    $scope.category = {
        CategoryId: 0,
        Name: '',
        Image: 'noimage.jpg',
        Orden: '',
        Comment: '',
        Pending: true,
        IsFinal: true,
        Active: true,
        Prefix: '',
        Translation: {
            Id: 0,
            Spanish: '',
            English: '',
            French: '',
            German: '',
            Language5: '',
            Language6: '',
            Language7: '',
            Language8: '',
            Active: true
        }//,
        //Catalogues: [{ CatalogId: 0, Name: 'Catalog', Active: true }]
    };    
    
    //$scope.selectCatalog = function (id)
    //{
    //    $scope.IdCatalog = id;
    //}

    $scope.initTabsets = function ()
    {
        $scope.showTabsetCategory=false;
        $scope.showTabsetModule = true;
    }

    $scope.updateCatalog = function (catalog) {

        catalogService.updateCatalog(catalog).then(function (response) {
            catalogService.getAll().then(function (response) {
                $scope.cata = response.data.value;

                $scope.catalogues = [$scope.cata.length]
                angular.forEach($scope.cata, function (value, key) {
                    $scope.catalogues[key] = { Name: value.Name };
                });
                
                $('#itemselect').find('span').eq(2).text(catalog.Name);
                $scope.steps.step1 = true;

                $scope.catalog = {
                    Active: true,
                    Image: 'img/prod.jpg',
                    Translation: {
                        Active: true
                    }
                };
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        },
        function (err) {
            $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
            $scope.pop();
        });
    };
    $scope.saveCatalog = function (catalog)
    {
        catalogService.saveCatalog(catalog).then(function (response) {
            $scope.toaster = {
                type: 'success',
                title: 'Success',
                text: 'The Catalog has been saved'
            };
            $scope.pop();
            $scope.loadCatalog();

            //$('#itemselect').find('span').eq(2).text(catalog.Name);
            $scope.steps.step1 = true;

            $scope.catalog = {
                Active: true,
                Image: 'img/prod.jpg',
                Translation: {
                    Active: true
                }
            };
            
            //$('select[name="cboCatalogue"]').append(new Option(response.data.Name, response.data.Id));
            //$('select[name="cboCatalogue"]').find('option[value="' + response.data.Id + '"]').attr("selected", true);
            //$scope.selectActionCatalog(response.data.Id);
            //$scope.Name = "";

        },
        function (err) {
            $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
            $scope.pop();
        });
    }
    $scope.deleteCatalog = function (id)
    {
        catalogService.removeCatalog(id.IdCatalog).then(function (response) {
            $scope.toaster = { type: 'success',title: 'Info', text: 'The Catalog has been removed' };
            $scope.pop();
            $scope.loadCatalog();
        },
        function (err) {
            $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
            $scope.pop();
        });
    }
    $scope.saveModule = function(module)
    {
        if ($scope.IsNew) {
            if ($scope.IdCatalog > 0) {
                $scope.module.Catalogues = [{ CatalogId: $scope.IdCatalog, Name: $scope.NameCatalog, Active: true }];

                catalogService.saveModule(module).then(function (response) {
                    $scope.toaster = { type: 'success', title: 'Success', text: 'The Module has been saved'};
                    $scope.pop();
                    $scope.steps.step1 = true;
                    $scope.loadTreeCatalog($scope.IdCatalog);
                },
                function (err) {
                    $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                    $scope.pop();
                });
            }
            else {
                $scope.toaster = {
                    type: 'error',
                    title: 'Info',
                    text: 'Selected Catalogue'
                };
                $scope.pop();
            }
        } else { //Modificar
            if ($scope.IdCatalog > 0) {
                //$scope.module.Catalogues = [{ CatalogId: $scope.IdCatalog, Name: $scope.NameCatalog, Active: true }];
                catalogService.editModule(module).then(function (response) {
                    $scope.toaster = { type: 'success', title: 'Info', text: 'The Module has been update' };
                    $scope.pop();
                    $scope.steps.step1 = true;
                    $scope.loadTreeCatalog($scope.IdCatalog);
                },
                function (err) {
                    $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                    $scope.pop();
                });
            }
            else {
                $scope.toaster = {
                    type: 'error',
                    title: 'Info',
                    text: 'Selected Catalogue'
                };
                $scope.pop();
            }


        }
        $scope.IsNew = true;
        $scope.module = {};
    };
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };
    $scope.saveCategory = function (category) {

        if ($scope.IsNew) {
            if ($scope.IdCatalog > 0) {
                //$scope.category.Modules = [{ ModuleId: $scope.IdModule, Name: 'Module', Active: true }];
                $scope.category.Modules = [$scope.module];
                catalogService.saveCategory(category).then(function (response) {
                    $scope.toaster = {
                        type: 'success',
                        title: 'Info',
                        text: 'The Category has been saved'
                    };
                    $scope.pop();
                    $scope.steps.step1 = true;
                    $scope.loadTreeCatalog($scope.IdCatalog);

                },
                function (err) {
                    $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                    $scope.pop();
                });
            }
            else {
                $scope.toaster = {
                    type: 'error',
                    title: 'Info',
                    text: 'Selected Catalogue'
                };
                $scope.pop();
            }
        }
        else { //Modificar
            if ($scope.IdCatalog > 0) {
                //$scope.category.Modules = [{ ModuleId: $scope.IdModule, Name: 'Module', Active: true }];
                //$scope.category.Modules = [$scope.module];
                catalogService.editCategory(category).then(function (response) {
                    $scope.toaster = {
                        type: 'success',
                        title: 'Info',
                        text: 'The Category has been saved'
                    };
                    $scope.pop();
                    $scope.steps.step1 = true;
                    $scope.loadTreeCatalog($scope.IdCatalog);
                },
                function (err) {
                    $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                    $scope.pop();
                });
            }
            else {
                $scope.toaster = {
                    type: 'error',
                    title: 'Info',
                    text: 'Selected Catalogue'
                };
                $scope.pop();
            }
        }
        $scope.IsNew = true;
        $scope.category = {Active: true, Pending: true, IsFinal: true };
        $scope.module = {};
        $scope.initTabsets();
    };

}]);
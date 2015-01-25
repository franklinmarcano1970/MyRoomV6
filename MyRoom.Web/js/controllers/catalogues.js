'use strict';

/* Controllers */
// Catalogues controller
app.controller('CataloguesController', ['$scope', '$http', '$state', 'catalogService', 'toaster', function ($scope, $http, $state, catalogService, toaster) {
    var IdCatalog = 0;
    //$scope.IdModule = 0
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
        Pending: true,
        Active: true,
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
        Pending: true,
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
        $scope.module = {};
        $scope.category = {};
        $scope.showTabsetCategory=false;
        $scope.showTabsetModule = true;
    }

    $scope.removeCatalogPopup = function () {

        if (!$scope.cata.selected) {
            $scope.toaster = { type: 'info', title: 'Info', text: 'Select a catalog' };
            $scope.pop();
            return
        }
        $('#deleteCatalog').modal({
            show: 'true'
        });
    }

    $scope.createCatalogPopup = function () {
        $scope.modify = false;
        $('#newCatalog').modal({
            show: 'true'
        });
    }

    $scope.editCatalogPopup = function () {
        
        if (!$scope.cata.selected)
        {
            $scope.toaster = { type: 'info', title: 'Info', text: 'Select a catalog' };
            $scope.pop();
            return
        }
        $scope.modify = true;
        $('#newCatalog').modal({
            show: 'true'
        });
        
        catalogService.getCatalog($scope.cata.selected.id).then(function (response) {
            $scope.catalog = JSON.parse(response.data);
        });
    }

    $scope.updateCatalog = function (catalog) {
        catalogService.updateCatalog(catalog).then(function (response) {
            $scope.loadCatalog();
            $scope.steps.step1 = true;
            $scope.catalog = {
                Active: true,
                Image: 'img/prod.jpg',
                Translation: {
                    Active: true
                }
            };

            $scope.toaster = { type: 'success', title: 'Success', text: 'the catalog has been updated' };
            $scope.pop();

        },
        function (err) {
            $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
            $scope.pop();
        });
    };
    $scope.saveCatalog = function (catalog)
    {
        debugger
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
                Pending: true,
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
            $scope.loadCatalog();
            $scope.toaster = { type: 'success', title: 'Info', text: 'The Catalog has been removed' };
            $scope.pop();
        },
        function (err) {
            $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
            $scope.pop();
        });
    }
    $scope.saveModule = function(mmodule)
    {
        if(!$scope.cata.selected)
        {
            $scope.toaster = { type: 'Info', title: 'Info', text: 'Select a Catalogue'};
            $scope.pop();
            return;
        }
       
        $scope.module.Catalogues = [{ CatalogId: $scope.IdCatalog, Name: $scope.NameCatalog, Active: true }];
        var moduleViewModel = createModuleVM($scope.module);
        if ($scope.IsNew) {
            catalogService.saveModule(moduleViewModel).then(function (response) {
                $scope.toaster = { type: 'success', title: 'Success', text: 'The Module has been saved'};
                $scope.pop();
                debugger
                initModule();
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        } else { //Modificar
            //$scope.module.Catalogues = [{ CatalogId: $scope.IdCatalog, Name: $scope.NameCatalog, Active: true }];
            catalogService.updateModule(mmodule).then(function (response) {
                $scope.toaster = { type: 'success', title: 'Info', text: 'The Module has been update' };
                $scope.pop();
                initModule();
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        }
        $scope.IsNew = true;
       
    };
    function createModuleVM(entity)
    {
        var vm = {};
        vm.Name = entity.Name;
        vm.Image = entity.Image;
        vm.ModuleActive = entity.Active;
        vm.Comment = entity.Comment;
        vm.Pending = entity.Pending;
        vm.Orden = entity.Orden;
        vm.Prefix = entity.Prefix;
        vm.Spanish = entity.Translation.Spanish;
        vm.English = entity.Translation.English;
        vm.French = entity.Translation.French;
        vm.German = entity.Translation.German;
        vm.TranslationActive = entity.Translation.Active;
        vm.Language5 = entity.Translation.Language5;

        vm.Language6 = entity.Translation.Language6;
        vm.Language7 = entity.Translation.Language7;
        vm.Language8 = entity.Translation.Language8;

        return vm;
    }

    function initModule() {
        $scope.steps.step1 = true;
        $scope.loadTreeCatalog($scope.IdCatalog);
        $scope.initTabsets();
        $scope.module = { Active: true, Pending: true, IsFinal: true };

    }
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };

    $scope.disableListItem = function (item) {
        if (item.type == 'category' && item.IsFinal)
            return false;

        return true;
    }

    $scope.saveCategory = function (category) {
        debugger
         if(!$scope.cata.selected)
        {
            $scope.toaster = { type: 'Info', title: 'Info', text: 'Select a Catalogue'};
            $scope.pop();
            return;
        }
        if ($scope.IsNew) {
            //$scope.category.Modules = [{ ModuleId: $scope.IdModule, Name: 'Module', Active: true }];

            //si no tiene parentcategory es modulo
            if (!$scope.category.IdParentCategory)
                $scope.category.Modules = [$scope.module];

            catalogService.saveCategory(category).then(function (response) {
                $scope.toaster = {
                    type: 'success',
                    title: 'Info',
                    text: 'The Category has been saved'
                };
                $scope.pop();
                $scope.steps.step1 = true;
                $scope.categoryItem = null;
                $scope.loadTreeCatalog($scope.IdCatalog);

            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });        
        }
        else { //Modificar
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
        $scope.IsNew = true;
        $scope.initTabsets();
        $scope.category = { Active: true, Pending: true, IsFinal: true };
    };

}]);
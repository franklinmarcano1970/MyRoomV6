'use strict';

/* Controllers */
// Catalogues controller
app.controller('CataloguesController', ['$scope', '$http', '$state', 'catalogService', 'toaster', 'ngWebBaseSettings', 'FileUploader', function ($scope, $http, $state, catalogService, toaster, ngWebBaseSettings, FileUploader) {
    var uploader = $scope.uploader = new FileUploader({
        //url: ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=1-0-0'
    });
    //$scope.rootFile = '/img/';
    //$scope.rootFileModule = '/img/';
    //$scope.rootFileCategory = '/img/';
    $scope.typeAction = 'module';
    $scope.activeCatalog = true;
    var imageCatalog = '';
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
        Image: 'img/no-image.jpg',
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
        Image: 'img/no-image.jpg',
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
        Image: 'img/no-image.jpg',
        Orden: '',
        Comment: '',
        Pending: true,
        IsFinal: true,
        Active: true,
        Prefix: '',
        CategoryItem: 0,
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
        }
    };    
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
        $scope.loadTreeCatalog($scope.IdCatalog);
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
        
        if ($scope.typeAction == 'catalog') {
            $scope.catalog.Image = $scope.file.name;
            var fr = new FileReader();
            fr.onload = function (e) {
                $('#imageCatalog')
                    .attr('src', e.target.result)
            }
            fr.readAsDataURL(fileItem._file);
        }
        if ($scope.typeAction == 'module') {
            $scope.module.Image = $scope.file.name;
            var fr = new FileReader();
            fr.onload = function (e) {
                $('#imageModule')
                    .attr('src', e.target.result)
            }
            fr.readAsDataURL(fileItem._file);
        }
        if ($scope.typeAction == 'category') {
            $scope.category.Image = $scope.file.name;
            var fr = new FileReader();
            fr.onload = function (e) {
                $('#imageCategory')
                    .attr('src', e.target.result)
            }
            fr.readAsDataURL(fileItem._file);
        }

        imageCatalog = $scope.category.Image;
    }
    
    $scope.assignCatalogToHotel = function () {
        $state.go('app.page.hotel_catalogues');
    };

    $scope.initTabsets = function ()
    {
        $scope.IsNew = true;
        $scope.module = {};
        $scope.category = {};
        $scope.module = { Image: 'img/no-image.jpg', Active: true };
        $scope.category = { Image: 'img/no-image.jpg', Active: true };
        $scope.showTabsetCategory=false;
        $scope.showTabsetModule = true;
        $scope.typeAction = 'module';
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
        $scope.catalog = {
            Image: 'img/no-image.jpg',
            Pending: true,
            Active: true,
        };
        $scope.modify = false;
        $scope.typeAction = 'catalog';
        $('#newCatalog').modal({
            show: 'true'
        });
    }

    $scope.editCatalogPopup = function () {
        $scope.typeAction = 'catalog';
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
            //if ($scope.catalog.Image != 'img/no-image.jpg')
            //    $scope.rootFile = '/images/' + $scope.catalog.CatalogId + '/';
            //else {
            //    $scope.rootFile = '/img/';
            //}
        });
    }

    $scope.updateCatalog = function (catalog) {
        catalogService.updateCatalog(catalog).then(function (response) {
            $scope.loadCatalog();
            $scope.steps.step1 = true;
            $scope.catalog = {
                Active: true,
                Image: 'img/no-image.jpg',
                Translation: {
                    Active: true
                }
            };
            //Para subir la imagen
            if ($scope.fileItem != null) {
                $scope.fileItem.url = ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=2-' + $scope.IdCatalog + '-0';
                uploader.uploadAll();
            }
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
        catalogService.saveCatalog(catalog).then(function (response) {
            var res = response;
            $scope.IdCatalog = res.data;
            //var uploaderMod = $scope.uploader = new FileUploader({
            //    url: 'ngWebBaseSettings.webServiceBase/api/files/Upload?var=2-' + res.data + '-0'
            //});
            $scope.toaster = {
                type: 'success',
                title: 'Success',
                text: 'The Catalog has been saved'
            };
            //Para subir la imagen
            if ($scope.fileItem != null) {
                $scope.fileItem.url = ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=2-' + res.data + '-0';
                uploader.uploadAll();
            }
            
                
            $scope.loadCatalog();
            $scope.pop();

            //$('#itemselect').find('span').eq(2).text(catalog.Name);
            $scope.steps.step1 = true;

            $scope.catalog = {
                Pending: true,
                Active: true,
                Image: 'img/no-image.jpg',
                Translation: {
                    Active: true
                }
            };
            initModule();
            $scope.sourceItems = {};
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
                if ($scope.fileItem === undefined)
                    $scope.loadTreeCatalog($scope.IdCatalog);
                else {
                    $scope.fileItem.url = ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=3-' + $scope.IdCatalog + '-' + response.data;
                    uploader.uploadAll();
                }

                $scope.toaster = { type: 'success', title: 'Success', text: 'The Module has been saved'};
                $scope.pop();
                initModule();
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        } else { //Modificar
            mmodule.Image = moduleViewModel.Image;
            //$scope.module.Catalogues = [{ CatalogId: $scope.IdCatalog, Name: $scope.NameCatalog, Active: true }];
            catalogService.updateModule(mmodule).then(function (response) {
                if ($scope.fileItem === undefined)
                    $scope.loadTreeCatalog($scope.IdCatalog);
                else {
                    $scope.fileItem.url = ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=3-' + $scope.IdCatalog + '-' + $scope.module.ModuleId;
                    uploader.uploadAll();
                }

                $scope.toaster = { type: 'success', title: 'Info', text: 'The Module has been update' };
                $scope.pop();
                initModule();
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        }
        //Para subir la imagen

        $scope.IsNew = true;
       
    };
    function createModuleVM(entity)
    {
        var vm = {};
        vm.Name = entity.Name;
        vm.Image = "/images/" + $scope.IdCatalog + "/modules/" + entity.Image;
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
        vm.CatalogId = entity.Catalogues[0].CatalogId;
        vm.CatalogName = entity.Catalogues[0].Name;
        return vm;
    }

    function initModule() {
        $scope.steps.step1 = true;
        $scope.loadTreeCatalog($scope.IdCatalog);
        $scope.initTabsets();
        $scope.module = { Image: 'img/no-image.jpg', Active: true, Pending: true, IsFinal: true };

    }
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };

    $scope.disableListItem = function (item) {
        if (item.type == 'category' && item.IsFinal)
            return false;

        return true;
    }

    $scope.saveCategory = function (category, obj) {
        
         if(!$scope.cata.selected)
        {
            $scope.toaster = { type: 'Info', title: 'Info', text: 'Select a Catalogue'};
            $scope.pop();
            return;
        }
        if ($scope.IsNew) {
            //$scope.category.Modules = [{ ModuleId: $scope.IdModule, Name: 'Module', Active: true }];

            //si no tiene parentcategory es modulo
            //if (!$scope.category.IdParentCategory)
            //    $scope.category.Modules = [$scope.module];

            //$scope.category.CategoryItem = $scope.CategoryItemId;
            var categoryViewModel = createCategoryVM($scope.category);
            catalogService.saveCategory(categoryViewModel).then(function (response) {
                $scope.toaster = {
                    type: 'success',
                    title: 'Info',
                    text: 'The Category has been saved'
                };
                if ($scope.fileItem === undefined)
                    $scope.loadTreeCatalog($scope.IdCatalog);
                else {
                    $scope.fileItem.url = ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=4-' + $scope.IdCatalog + '-' + response.data.CategoryId;
                    uploader.uploadAll();
                    $scope.fileItem = undefined;
                }
                $scope.pop();
                $scope.steps.step1 = true;
                $scope.categoryItem = null;
                
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

                if ($scope.fileItem === undefined)
                    $scope.loadTreeCatalog($scope.IdCatalog);
                else {
                    $scope.fileItem.url = ngWebBaseSettings.webServiceBase + 'api/files/Upload?var=4-' + $scope.IdCatalog + '-' + $scope.category.CategoryId;
                    uploader.uploadAll();
                }
                $scope.pop();
                $scope.steps.step1 = true;
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });            
        }
        //Para subir la imagen
        //uploader.uploadAll();
        $scope.IsNew = true;
        $scope.initTabsets();
        $scope.category = { Image: 'img/no-image.jpg', Active: true, Pending: true, IsFinal: true };
    };

    function createCategoryVM(entity) {

        var vm = {};
        vm.Name = entity.Name;
        vm.Image = "/images/" + $scope.IdCatalog + "/categories/" + entity.Image;
        vm.CategoryActive = entity.Active;
        vm.Comment = entity.Comment;
        vm.Pending = entity.Pending;
        vm.Orden = entity.Orden;
        vm.Prefix = entity.Prefix;
        vm.IdParentCategory = entity.IdParentCategory;
        vm.CategoryItem = entity.CategoryItem;
        vm.IsFirst = entity.IsFirst;
        vm.IsFinal = entity.IsFinal;

        vm.Spanish = entity.Translation.Spanish;
        vm.English = entity.Translation.English;
        vm.French = entity.Translation.French;
        vm.German = entity.Translation.German;
        vm.TranslationActive = entity.Translation.Active;

        vm.Language5 = entity.Translation.Language5;
        vm.Language6 = entity.Translation.Language6;
        vm.Language7 = entity.Translation.Language7;
        vm.Language8 = entity.Translation.Language8;
        vm.ModuleId = $scope.currentModule.ModuleId;
        vm.ModuleName = $scope.currentModule.Name;
        vm.CatalogId = $scope.cata.selected.id;
        return vm;
    }
}]);
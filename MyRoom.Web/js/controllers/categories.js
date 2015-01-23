'use strict';

/* Controllers */
// Categories controller
app.controller('CategoriesController', ['$scope', '$http', '$state', function ($scope, $http, $state) {
    $scope.Mensaje = "";
    $scope.categories = [];
    $scope.items = [];
    $scope.IsNewRecord = 1;
    loadRecords();

    function loadRecords() {
        

        var CategoriesList = $http.get(serviceBase + "odata/Categories");

        CategoriesList.then(function (pl) {
            var myObj = pl.data;
            $scope.items = myObj.value;
            $scope.categories = myObj.value;
            //var i = 0;
        },
        function (errorPl) {
            $log.error('failure loading categories', errorPl);
        });
    };

    function clearCategory(scope) {
        scope.IsNewRecord = 1;
        scope.Id = 0;
        scope.categoryName = "";
        scope.isactive = false;
        scope.ispending = false;
        scope.categoryImage = "",
        scope.categoryOrder = 0
        $scope.Mensaje = "Save Success";
    };

    $scope.saveCategory = function (scope) {
        var Category = {
            Id: 1,
            Name: scope.categoryName,
            Image: scope.categoryImage,
            IdTranslationName: 1,
            Orden: scope.categoryOrder,
            Active: scope.isactive,
            Pending: scope.ispending
        };
        //If the flag is 1 the it si new record
        if ($scope.IsNewRecord === 1) {

            var request = $http({
                method: "post",
                url: serviceBase + "odata/Categories",
                data: Category
            });

            var post = request;
            post.then(function (pl) {
                $scope.Id = pl.data.Id;
                scope.steps.percent = 100;
                clearCategory(scope);
                loadRecords();
            }, function (err) {
                console.log("Err" + err);
            });
        } else { //Else Edit the record
            var request = $http({
                method: "put",
                url: serviceBase + "odata/Hotel/(" + $scope.Id + ")",
                data: user
            });
            var put = request;
            put.then(function (pl) {

            }, function (err) {
                console.log("Err" + err);
            });
        }

    };

}]);
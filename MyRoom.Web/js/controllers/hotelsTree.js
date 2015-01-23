app.controller('HotelsTreeController', ['$scope', '$http', '$state', function ($scope, $http, $state) {
    var apple_selected, tree, treedata_avm = [], treedata_geography;
    $scope.IdCatalog = 0;
    $scope.IdHotel = 0;
    $scope.my_tree_handler = function (branch) {
        var _ref;
        $scope.IdHotel = branch.data.Id;

        $scope.output = "You selected: " + branch.label;
        if ((_ref = branch.data) != null ? _ref.description : void 0) {
            return $scope.output += '(' + branch.data.description + ')';
        }
    };
    apple_selected = function (branch) {
        return $scope.output = "APPLE! : " + branch.label;
    };

    treedata_avm = [
    {
        label: 'Hotel1',
        children: [
          {
              label: 'Catalog1',
              data: {
                  description: "description"
              }
          }, 
        ]
    }, {
        label: 'Hotel2',
        data: {
            definition: "description",
            data_can_contain_anything: true
        },
    },
    ];

    loadRecords();

    function loadRecords()
    {

        //Cargamos los hoteles con los catalogos añadidos
        //odata/Hotels?$expand=ActiveHotelCatalogue
        $scope.my_tree = tree = {};
        $scope.my_data = [{}]
        treedata_avm = [{}];
        var getHotel = $http.get(serviceBase + "odata/Hotels");

        getHotel.then(function (pl) {
            var myObj = pl.data;
            $scope.hotels = myObj.value;
            console.info($scope.hotels);
            var items = [$scope.hotels.length];
            var i = 0;
            for (i; i < $scope.hotels.length; i++) {
                getChildren($scope.hotels[i].Id);
                
            }            
        },
        function (errorPl) {
            $log.error('failure loading hotels', errorPl);
        });

        var getCatalogue = $http.get(serviceBase + "odata/Catalogues");
        getCatalogue.then(function (pl) {
            var myObj = pl.data;
            $scope.catalogues = myObj.value;
        },
        function (errorPl) {
            $log.error('failure loading catalogue', errorPl);
        });
        /////////////////////////////////////////////////
    };
  
    $scope.my_data = treedata_avm;
    getChildren = function (id) {
        var i = 0;
        var items = [{}];
        //Hotels(1)/ActiveHotelCatalogue?$expand=Catalogues
        var getCatalogue = $http.get(serviceBase + "odata/Hotels(" + id + ")/ActiveHotelCatalogue?$expand=Catalogues");
        getCatalogue.then(function (pl) {
            var myObj = pl.data;
            //for (i; i < myObj.value.length; i++) {
            //items = [{ "label": myObj.value[0].Catalogues.Name }];

            treedata_avm[i] = {
                label: $scope.hotels[i].Name,
                data: [{ "Id": $scope.hotels[i].Id }],
                children: [{ "label": myObj.value[0].Catalogues.Name }]
            }

            $scope.my_data = treedata_avm;

            //}
        },
        function (errorPl) {
            $log.error('failure loading users', errorPl);
        });
        console.info(items)
     
    }
    $scope.getCatalog = function (id) {
        var getCatalog = $http.get(serviceBase + "odata/Catalogues/(" + id + ")");

        angular.forEach($scope.catalogues, function (res, key) {
            if (res.Id == id) {
                $scope.IdCatalog = res.Id;
            }
        });

        getCatalog.then(function (pl) {
            var res = pl.data;

            $scope.IdCatalog = res.Id;

        },
        function (errorPl) {
            console.log('failure loading Users', errorPl);
        });
    };
    $scope.activeCatalog = function () {
        var ActiveHotelCatalog = {
            IdHotel: $scope.IdHotel,
            IdCatalogue: $scope.IdCatalog,
            Active: true
        };
        var request = $http({
            method: "post",
            url: serviceBase + "odata/ActiveHotelCatalogue",
            data: ActiveHotelCatalog
        });

        var post = request;
        post.then(function (pl) {
            loadRecords();
        }, function (err) {
            console.log("Err" + err);
        });


    };
    $scope.deactiveCatalog = function () {
        var ActiveHotelCatalog = {
            IdHotel: $scope.IdHotel,
            IdCatalogue: $scope.IdCatalog,
            Active: false
        };
        var request = $http({
            method: "put",
            url: serviceBase + "odata/ActiveHotelCatalogue/(" + IdCatalog + ")",
            data: ActiveHotelCatalog
        });

        var post = request;
        post.then(function (pl) {
            loadRecords();
        }, function (err) {
            console.log("Err" + err);
        });


    };
    $scope.try_changing_the_tree_data = function () {
        if ($scope.my_data === treedata_avm) {
            return $scope.my_data = treedata_geography;
        } else {
            return $scope.my_data = treedata_avm;
        }
    };
    $scope.my_tree = tree = {};
    $scope.try_async_load = function () {
        $scope.my_data = [];
        $scope.doing_async = true;
        return $timeout(function () {
            if (Math.random() < 0.5) {
                $scope.my_data = treedata_avm;
            } else {
                $scope.my_data = treedata_geography;
            }
            $scope.doing_async = false;
            return tree.expand_all();
        }, 1000);
    };
    return $scope.try_adding_a_branch = function () {
        var b;
        b = tree.get_selected_branch();
        return tree.add_branch(b, {
            label: 'New Branch',
            data: {
                something: 42,
                "else": 43
            }
        });
    };
}])
;
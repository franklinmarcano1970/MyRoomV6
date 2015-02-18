﻿'use strict';

/* Controllers */
// Rooms controller
app.controller('RoomsListController', ['$scope', '$http', '$state', 'roomService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', function ($scope, $http, $state, roomService, DTOptionsBuilder, DTColumnDefBuilder, toaster) {
    $scope.rooms = {};
    $scope.currentRoomtId = 0;;

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
        $scope.createRoom = function () {
            $state.go('app.page.room_create');
        }
        $scope.getAll = function () {
            roomService.getAll().then(function (response) {
                $scope.rooms = response.data;

            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();

            });
        };

        $scope.selectRoom = function (id) {
            $scope.currentRoomId = id;
            $('#deleteRoom').modal({
                show: 'true'
            });
        }

        $scope.modifyRoom = function (id) {
            $scope.currentRoomId = id;
            $state.go('app.page.room_edit', { "id": id });
        }

        $scope.removeRoom = function (id) {
            roomService.removeRoom(id).then(function (response) {
                $scope.toaster = {
                    type: 'success',
                    title: 'Info',
                    text: 'The room has been removed'
                };
                $scope.room = {
                    Name: '',
                    Number: '',
                    HotelId: 0,
                    IsEmpty: true,
                    IsReadyForUse: true,
                    Standard: true,
                    Premium: false,
                    Active: true,
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
app.controller('RoomsController', ['$scope', '$http', '$state', 'roomService', 'toaster', '$timeout', function ($scope, $http, $state, roomService, toaster, $timeout) {
    $scope.room = {
        Name: '',
        Number: '',
        HotelId: 0,
        IsEmpty: true,
        IsReadyForUse: true,
        Standard: true,
        Premium: false,
        Active: true,
    };

    //$scope.toaster = {
    //    type: 'success',
    //    title: 'Info',
    //    text: 'The User has been saved'
    //};
   
    
    $scope.Mensaje = "";
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };
 
    $scope.saveRoom = function () {
        if ($state.current.name == "app.page.room_create") {
            roomService.saveRoom($scope.room).then(function (response) {
                $scope.room = {
                    Name: '',
                    Number: '',
                    HotelId: 0,
                    IsEmpty: true,
                    IsReadyForUse: true,
                    Standard: true,
                    Premium: false,
                    Active: true,
                };
                $timeout(function () {
                    $scope.toaster = {
                        type: 'success',
                        title: 'Success',
                        text: 'The Room has been saved'
                    };
                    $state.go('app.page.rooms_list');
                    $scope.pop();
                }, 2000);
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        }
        else {
            roomService.updateRoom($scope.room).then(function (response) {
                $scope.toaster = {
                    type: 'success',
                    title: 'Success',
                    text: 'The Room has been updated'
                };
                $timeout(function () {
                    $scope.pop();

                }, 1000).then(function () {
                });
                $state.go('app.page.rooms_list');
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
}]);

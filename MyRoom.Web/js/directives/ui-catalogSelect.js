angular.module('app')
  .directive('catalogSelect', ['$animate', function ($animate) {
      return {
            restrict: 'E',
            templateUrl: '../../tpl/partials/catalog-select.html',
            controller: function() {
                      
            },
            controllerAs: 'catalogCtrl'
      };
  }]);

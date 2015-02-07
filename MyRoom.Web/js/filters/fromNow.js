'use strict';

/* Filters */
// need load the moment.js to use this filter. 
angular.module('app')
  .filter('fromNow', function() {
    return function(date) {
      return moment(date).fromNow();
    }
  });

//filtro para poner la primera letra en mayúscula
app.filter("capitalize", function () {
    return function (text) {
        if (text != null) {
            return text.substring(0, 1).toUpperCase() + text.substring(1);
        }
    }
});

app.filter("ischecked", function () {
    return function (array, cantElem) {
        var items = $("input[name='post[]']:checked").length;
        if (items > cantElem) {
            return false;
        }
        return true;
    }
})

app.filter("ischeckedArray", function () {
    return function (array) {
        var items = $("input[name='post[]']:checked").length;

        return items;
    }
})

app.filter("ItemsCheckedTreeNode", ['$filter', function ($filter, res) {
    var res;
    if (res === undefined)
        res = [];

    return function (tree) {
        angular.forEach(tree, function(item) { 
            var copy = angular.fromJson(angular.toJson(item))
            var id = 0;
            if (item.type == 'module')   id = item.ModuleId;
            if (item.type == 'category') {
                id = item.CategoryId;
            }

            if (item.type == 'product') id = item.ProductId;
            item.ischecked = true;
            res.push({
                id: id,
                type: item.type,
            });
            $filter('ItemsCheckedTreeNode')(item.children, res)
        });
        return res;
    }
}])
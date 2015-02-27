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

//app.filter("ischecked", function () {
//    return function (array, cantElem) {
//        var items = $("input[name='post[]']:checked").length;
//        if (items > cantElem) {
//            return false;
//        }
//        return true;
//    }
//})

//app.filter("ischeckedArray", function () {
//    return function (array) {
//        var items = $("input[name='post[]']:checked").length;

//        return items;
//    }
//})

app.filter("ItemsCheckedTreeNode", ['$filter', function ($filter) {
    var itemParent;
    return function (tree) {
        var id = 0;
        if (tree.children !== undefined) {
            if (tree.type == 'module') id = tree.ModuleId;
            if (tree.type == 'category') id = tree.CategoryId;
            
            if (tree.type == 'product') id = tree.ProductId;

            
            itemParent = tree;
            tree = tree.children;


        }
        angular.forEach(tree, function (item) {
            // var copy = angular.fromJson(angular.toJson(item))   
            if (item.type !== undefined) {
                if (item.type == 'module') id = item.ModuleId;
                if (item.type == 'category') {
                    id = item.CategoryId;
                }

                if (item.type == 'product') id = item.ProductId;
                if (itemParent.IsChecked) {
                    item.IsChecked = true;
                }
                else {
                    item.IsChecked = false;
                }

                itemParent = item;
                if (item.children != undefined || item.children != null)
                    $filter('ItemsCheckedTreeNode')(item.children)

            }
        });
       
    }
}])
app.filter("GetCheckedTreeNode", ['$filter', function ($filter, res) {
    var res = [];
    return function (tree, res) {
        var id = 0;
        angular.forEach(tree, function (item) {
            // var copy = angular.fromJson(angular.toJson(item))   
            if (item.IsChecked) {
                if (item.type == 'module') id = item.ModuleId;
                if (item.type == 'category') {
                    id = item.CategoryId;
                }

                if (item.type == 'product') id = item.ProductId;
                res.push({
                    id: id,
                    type: item.type
                });
            }
            $filter('GetCheckedTreeNode')(item.children, res)
        });
        return res;
    }
}])
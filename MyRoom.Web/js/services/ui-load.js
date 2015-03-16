'use strict';

/**
 * 0.1.1
 * Deferred load js/css file, used for ui-jq.js and Lazy Loading.
 * 
 * @ flatfull.com All Rights Reserved.
 * Author url: http://themeforest.net/user/flatfull
 */

angular.module('ui.load', [])
    .factory('roomService', ['$http', '$q', function ($http, $q) {
        function getAll() {
            var deferred = $q.defer();
            return $http.get(serviceBase + 'api/rooms').success(function (response) {
                deferred.resolve(response);
            }, function (err) {
                deferred.reject(err);
            });

            return deferred.promise;
        };
        function saveRoom(room) {
            var deferred = $q.defer();
            return $http.post(serviceBase + 'api/rooms', room).success(function (response) {
                deferred.resolve(response);
            }, function (err) {
                deferred.reject(err);
            });

            return deferred.promise;
        };

        function updateRoom(room) {
            var deferred = $q.defer();
            return $http.put(serviceBase + 'api/rooms/', room).success(function (response) {
                deferred.resolve(response);
            }, function (err) {
                deferred.reject(err);
            });

            return deferred.promise;
        };

        function getRoom(id) {
            var deferred = $q.defer();
            return $http.get(serviceBase + 'api/rooms/' + id).success(function (response) {
                deferred.resolve(response);
            }, function (err) {
                deferred.reject(err);
            });

            return deferred.promise;
        };

        function removeRoom(id) {
            var deferred = $q.defer();
            return $http.delete(serviceBase + 'api/rooms/' + id).success(function (response) {
                deferred.resolve(response);
            }, function (err) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        return {
            getAll: getAll,
            saveRoom: saveRoom,
            removeRoom: removeRoom,
            getRoom: getRoom,
            updateRoom: updateRoom
        };
    }])

    .factory('departmentService', ['$http', '$q', function ($http, $q) {
    	function getAll() {
    	  //  var deferred = $q.defer();
    	    return $http.get(serviceBase + 'api/departments');//.success(function (response) {
    	        //deferred.resolve(response);
    	    //}, function (err) {
    	    //    deferred.reject(err);
    	    //});

    	    //return deferred.promise;
    	};
    	function saveDepartment(department) {
    	    var deferred = $q.defer();
    	    return $http.post(serviceBase + 'api/departments', department).success(function (response) {
    	        deferred.resolve(response);
    	    }, function (err) {
    	        deferred.reject(err);
    	    });

    	    return deferred.promise;
    	};

    	function updateDepartment(department) {
    	    var deferred = $q.defer();
    	    return $http.put(serviceBase + 'api/departments/', department).success(function (response) {
    	        deferred.resolve(response);
    	    }, function (err) {
    	        deferred.reject(err);
    	    });

    	    return deferred.promise;
    	};

    	function getDepartment(id) {
    	    return $http.get(serviceBase + 'api/departments/' + id);

    	};

    	function removeDepartment(id) {
    	    var deferred = $q.defer();
    	    return $http.delete(serviceBase + 'api/departments/' + id).success(function (response) {
    	        deferred.resolve(response);
    	    }, function (err) {
    	        deferred.reject(err);
    	    });
    	    return deferred.promise;
    	};
   
    	return {
    	    getAll: getAll,
    	    saveDepartment: saveDepartment,
    	    removeDepartment: removeDepartment,
    	    getDepartment: getDepartment,
    	    updateDepartment: updateDepartment
    	};
    }])
	.factory('hotelService', ['$http', '$q', function ($http, $q) {
		function getAll() {
		    return $http.get(serviceBase + 'api/Hotels');
		};
		function assignCatalog(hotelcatalogVm) {
		    var deferred = $q.defer();
		    return $http.post(serviceBase + 'api/hotels/catalogues', hotelcatalogVm).success(function (response) {
		        deferred.resolve(response);
		    }, function (err) {
		        deferred.reject(err);
		    });

		    return deferred.promise;
		};		
		function getUserHotelId(idUser, IdHotel) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'odata/RelUserHotels?$filter=IdUser eq ' + idUser + ' and IdHotel eq ' + IdHotel).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		}
		function getHotelCatalogId(idUser) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'api/hotels/catalog/' + idUser).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		}
		function saveHotel(hotel, fileUpload) {
        	var deferred = $q.defer();
			return $http.post(serviceBase + 'api/Hotels', hotel).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};

		function getCatalogAssignedByHotelId(hotelId) {
		    var deferred = $q.defer();
		    return $http.get(serviceBase + 'api/hotels/catalog/'+ hotelId).success(function (response) {
		        deferred.resolve(response);
		    }, function (err) {
		        deferred.reject(err);
		    });

		    return deferred.promise;
		};

		
		//function saveActiveHotelCatalog(activeHotelCatalog) {
		//    var deferred = $q.defer();
		//    return $http.post(serviceBase + 'api/falta cambiar esto por el Api Real', activeHotelCatalog).success(function (response) {
		//        deferred.resolve(response);
		//    }, function (err) {
		//        deferred.reject(err);
		//    });

		//    return deferred.promise;
		//};
		//obtengo los permisos que le corresponde a un usuario en particular
		function getUserPermissions(userid) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'api/permissions/user/' + userid).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};

		function updateHotel(hotel) {
			var deferred = $q.defer();
			return $http.put(serviceBase + 'api/Hotels/', hotel).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};

		function getHotel(id) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'api/Hotels/' + id).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};
	
		function removeHotel(id) {
			var deferred = $q.defer();
			return $http.delete(serviceBase + 'api/Hotels/' + id).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});
			return deferred.promise;
		};
		function removeUserHotel(id) {
			var deferred = $q.defer();
			return $http.delete(serviceBase + 'odata/RelUserHotels(' + id + ')').success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});
			return deferred.promise;
		};

		function saveUserPermission(permissions) {
			var deferred = $q.defer();
			return $http.post(serviceBase + 'api/permissions/userhotel', permissions).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		}
		
		function getProductsActivated(hotelId) {
		    var deferred = $q.defer();
		    return $http.get(serviceBase + 'api/hotels/products/' + hotelId).success(function (response) {
		        deferred.resolve(response);
		    }, function (err) {
		        deferred.reject(err);
		    });

		    return deferred.promise;
		}

		function saveActiveProduct(assignhotelelements) {
		    var deferred = $q.defer();
		    return $http.post(serviceBase + 'api/hotels/assignhotelelements', assignhotelelements).success(function (response) {
		        deferred.resolve(response);
		    }, function (err) {
		        deferred.reject(err);
		    });

		    return deferred.promise;
		}

		return {
			getAll: getAll,
			saveHotel: saveHotel,
			saveUserPermission: saveUserPermission,
			getUserHotelId: getUserHotelId,
			removeHotel: removeHotel,
			getHotelCatalogId: getHotelCatalogId,
			removeUserHotel: removeUserHotel,
			getCatalogAssignedByHotelId: getCatalogAssignedByHotelId,
			getHotel: getHotel,
			updateHotel: updateHotel,
			assignCatalog: assignCatalog,
			saveActiveProduct: saveActiveProduct,
			getProductsActivated: getProductsActivated
		};
	}])
	.factory('catalogService', ['$http', '$q', function ($http, $q) {
			 function getAll() {
				 var deferred = $q.defer();
				 return $http.get(serviceBase + 'api/Catalogues').success(function (response) {
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function getProductsByCategory(categoryId) {
			     var deferred = $q.defer();
			     return $http.get(serviceBase + 'api/categoryproduct/' + categoryId).success(function (response) {
			         deferred.resolve(response);
			     }, function (err) {
			         deferred.reject(err);
			     });

			     return deferred.promise;

			 };
			 function getCatalogComplex(catalogId, withproducts, activemod, activecategory, hotelid) {
				 var deferred = $q.defer();
				 return $http.get(serviceBase + 'api/Catalogues/' + catalogId + "/?withproducts=" + withproducts + "&activemod=" + activemod + "&activecategory=" + activecategory + "&hotelid=" + hotelid).success(function (response) {
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };

			 function saveAssingProduct(catalogAssignProductsVm) {
				 var deferred = $q.defer();
				 return $http.post(serviceBase + 'api/categories/assignproducts', catalogAssignProductsVm).success(function (response) {
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function saveCatalog(catalog) {
				 var deferred = $q.defer();
				 return $http.post(serviceBase + 'api/Catalogues', catalog).success(function (response) {
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function saveCatalogUser(catalogUserVm) {
				 var deferred = $q.defer();
				 return $http.post(serviceBase + 'api/catalogues/user', catalogUserVm).success(function (response) {
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function saveModuleUser(relUserModule) {
				 var deferred = $q.defer();
				 return $http.post(serviceBase + 'odata/RelUserModules', relUserModule).success(function (response) {
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function saveCategoryUser(relUserCategory) {
				 var deferred = $q.defer();
				 return $http.post(serviceBase + 'odata/RelUserCategory', relUserCategory).success(function (response) {
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function updateCatalog(catalog) {
				 var deferred = $q.defer();
				 return $http.put(serviceBase + 'api/Catalogues/', catalog).success(function (response) {
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function getCatalog(id) {
			     var deferred = $q.defer();
			     return $http.get(serviceBase + 'api/Catalogues/catalogbyid/' + id).success(function (response) {
			         deferred.resolve(response);
			     }, function (err) {
			         deferred.reject(err);
			     });

			     return deferred.promise;
			 };

			 function saveModule(mmodule) {
				 var deferred = $q.defer();
				 return $http.post(serviceBase + 'api/modules', mmodule).success(function (response) {
					 //$scope.loadTreeCatalog($scope.IdCatalog);
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };

			 function updateModule(mmodule) {
				 var deferred = $q.defer();
				 return $http.put(serviceBase + 'api/modules/', mmodule).success(function (response) {
					 //$scope.loadTreeCatalog($scope.IdCatalog);
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function saveCategory(category) {
				 var deferred = $q.defer();
				 return $http.post(serviceBase + 'api/categories', category).success(function (response) {
					 //$scope.loadTreeCatalog($scope.IdCatalog);
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function editCategory(category) {
				 var deferred = $q.defer();
				 return $http.put(serviceBase + 'api/categories/', category).success(function (response) {
					 //$scope.loadTreeCatalog($scope.IdCatalog);
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });

				 return deferred.promise;
			 };
			 function removeCatalog(id) {
				 var deferred = $q.defer();
				 return $http.delete(serviceBase + 'api/catalogues/' + id ).success(function (response) {
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });
				 return deferred.promise;
			 };
			 function removeCategory(id) {
				 var deferred = $q.defer();
				 return $http.delete(serviceBase + 'api/categories/' + id).success(function (response) {
					 //$scope.loadTreeCatalog($scope.IdCatalog);
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });
				 return deferred.promise;
			 };
			 function removeModule(id) {
				 var deferred = $q.defer();
				 return $http.delete(serviceBase + 'api/modules/' + id ).success(function (response) {
					 //$scope.loadTreeCatalog($scope.IdCatalog);
					 deferred.resolve(response);
				 }, function (err) {
					 deferred.reject(err);
				 });
				 $scope.loadTreeCatalog($scope.IdCatalog);
				 return deferred.promise;
			 };
			 return {
				 getAll: getAll,
				 saveAssingProduct: saveAssingProduct,
				 saveCatalog: saveCatalog,
				 saveCatalogUser: saveCatalogUser,
				 saveModuleUser: saveModuleUser,
				 saveCategoryUser: saveCategoryUser,
				 updateCatalog: updateCatalog,
				 saveModule: saveModule,
				 updateModule: updateModule,
				 saveCategory: saveCategory,
				 editCategory: editCategory,
				 removeCatalog: removeCatalog,
				 removeCategory: removeCategory,
				 removeModule: removeModule,
				 getCatalogComplex: getCatalogComplex,
				 getProductsByCategory: getProductsByCategory,
				 getCatalog: getCatalog
			 };
	   }])
	  .factory('accountService', ['$http', '$q', function ($http, $q) {
		function getAll() {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'api/account/users', { cache: false }).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};
		function getCatalogUserId(id) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'odata/RelUserCatalogues?$filter=IdUser eq ' + id).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};
		
		//obtengo los permisos que le corresponde a un usuario en particular
		function getUserPermissions(userid) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'api/permissions/user/' + userid ).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};

		function getHotelUserId(id) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'odata/RelUserHotels?$filter=IdUser eq ' + id).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};

		function removeUserMenu(id) {
			var deferred = $q.defer();
			return $http.delete(serviceBase + 'odata/RelUserAccess(' + id + ')').success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});
			return deferred.promise;
		};
		function getModuleUserId(id) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'odata/RelUserModules?$filter=IdUser eq ' + id).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};
		//obtiene todos los permisos disponibles
		function getAllMenuAccess() {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'api/permissions', { cache: true }).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};

		function savePermissions(user) {
			var deferred = $q.defer();
			return $http.post(serviceBase + 'api/permissions', user).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		}

		//function saveUserMenuAccess(RelUserAccess) {
		//	var deferred = $q.defer();
		//	return $http.post(serviceBase + 'odata/RelUserAccess', RelUserAccess).success(function (response) {
		//		deferred.resolve(response);
		//	}, function (err) {
		//		deferred.reject(err);
		//	});

		//	return deferred.promise;
		//}
		function getUser(id) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'api/account/' + id).success(function (response) {		
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};

		function saveRegistration(registration) {
			var deferred = $q.defer();
			return $http.post(serviceBase + 'api/account/register', registration).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};

		function updateUser(account) {
			var deferred = $q.defer();
			return $http.put(serviceBase + 'api/account/', account).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};
		function removeUser(id) {
			var deferred = $q.defer();
			return    $http.delete  (serviceBase + 'api/account/'+id, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});
			return deferred.promise;
		};

		  //obtengo los permisos que le corresponde a un usuario sobre un hotel en particular
		function getUserHotelPermissions(userid) {
			var deferred = $q.defer();
			return $http.get(serviceBase + 'api/permissions/userhotel/' + userid).success(function (response) {
				deferred.resolve(response);
			}, function (err) {
				deferred.reject(err);
			});

			return deferred.promise;
		};
		return {
			saveRegistration: saveRegistration,
			getAll: getAll,
			getCatalogUserId: getCatalogUserId,
			getHotelUserId: getHotelUserId,
			getModuleUserId: getModuleUserId,
			getAllMenuAccess: getAllMenuAccess,
			savePermissions: savePermissions,
			removeUser: removeUser,
			removeUserMenu: removeUserMenu,
			getUser: getUser,
			getUserPermissions: getUserPermissions,
			updateUser: updateUser,
			getUserHotelPermissions: getUserHotelPermissions
		};
	  }])
	.factory('productService', ['$http', '$q', function ($http, $q) {
		 function getAll() {
			 var deferred = $q.defer();
			// return $http.get(serviceBase + 'odata/Products', {cache:true}).success(function (response) {
			 return $http.get(serviceBase + 'api/products').success(function (response) {
				 deferred.resolve(response);
			 }, function (err) {
				 deferred.reject(err);
			 });

			 return deferred.promise;
		 };

		 function getProduct(id) {
			 var deferred = $q.defer();
			 return $http.get(serviceBase + 'api/products/' + id).success(function (response) {
				 deferred.resolve(response);
			 }, function (err) {
				 deferred.reject(err);
			 });

			 return deferred.promise;
		 };

		 function saveProduct(product) {
			 var deferred = $q.defer();
			// product['Order'] =  product["Order"].toString();
			 return $http.post(serviceBase + 'api/products', product).success(function (response) {				 
				 deferred.resolve(response);
			 }, function (err) {
				 deferred.reject(err);
			 });
			 return deferred.promise;
		 };
		 function updateProduct(product) {
		     var deferred = $q.defer();
		     // product['Order'] =  product["Order"].toString();
		     return $http.put(serviceBase + 'api/products', product).success(function (response) {
		         deferred.resolve(response);
		     }, function (err) {
		         deferred.reject(err);
		     });
		     return deferred.promise;
		 };

		 function saveAssingProductCatalog(RelCategoryProducts) {
			 var deferred = $q.defer();
			 return $http.post(serviceBase + 'odata/RelCategoryProducts', RelCategoryProducts).success(function (response) {
				 deferred.resolve(response);
			 }, function (err) {
				 deferred.reject(err);
			 });
			 return deferred.promise;
		 };

		 function removeProduct(id) {
			 var deferred = $q.defer();
			 return $http.delete(serviceBase + 'api/products/' + id ).success(function (response) {
				 deferred.resolve(response);
			 }, function (err) {
				 deferred.reject(err);
			 });
			 return deferred.promise;
		 };

		 return {
			 saveProduct: saveProduct,
			 saveAssingProductCatalog: saveAssingProductCatalog,
			 getAll: getAll,
			 removeProduct: removeProduct,
			 getProduct: getProduct,
			 updateProduct: updateProduct
		 };
	 }])
	.factory('authInterceptorService', ['$q', '$injector', '$location', 'localStorageService', function ($q, $injector, $location, localStorageService) {
		var authInterceptorServiceFactory = {};
		var _request = function (config) {
            config.headers = config.headers || {};
           
            var authData = localStorageService.get('authorizationData');
      
		    if (authData) {
		        config.headers.Authorization = 'Bearer ' + authData.token; 		  
		    }
            else
		    {
		        $location.path('/access/signin');
		    }
		
			return config;
		}

		var _responseError = function (rejection) {
			if (rejection.status === 401) {
				var authService = $injector.get('authService');
				var authData = localStorageService.get('authorizationData');
//				debugger
			//	if (authData) {
//				    if (authData.useRefreshTokens) {				  
	//			        $location.path('/access/signin');
				        //return $q.reject(rejection);
		//			}
				//}
				authService.logOut();
				$location.path('/access/signin');

			}
			return $q.reject(rejection);
		}

		authInterceptorServiceFactory.request = _request;
		authInterceptorServiceFactory.responseError = _responseError;

		return authInterceptorServiceFactory;
	}])
	.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {
		var serviceBase = ngAuthSettings.apiServiceBaseUri;
		var authServiceFactory = {};

		var _authentication = {
			isAuth: false,
			username: "",
			useRefreshTokens: true
		};

		var _login = function (loginData) {
			
			var data = "grant_type=password&username=" + loginData.username + "&password=" + loginData.password;

//		   if (loginData.useRefreshTokens) {
	//	        data = data + "&client_id=" + ngAuthSettings.clientId;
		//    }

			var deferred = $q.defer();

			$http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

				if (loginData.useRefreshTokens) {
					localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.username, refreshToken: response.refresh_token, useRefreshTokens: true });
				}
				else {
					localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.username, refreshToken: "", useRefreshTokens: false });
				}

				_authentication.isAuth = true;
				_authentication.username = loginData.username;
				_authentication.useRefreshTokens = loginData.useRefreshTokens;

				deferred.resolve(response);

			}).error(function (err, status) {
				_logOut();
				deferred.reject(err);
			});

			return deferred.promise;

		};

		var _refreshToken = function () {
			var deferred = $q.defer();

			var authData = localStorageService.get('authorizationData');

			if (authData) {

				if (authData.useRefreshTokens) {

					var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;

					localStorageService.remove('authorizationData');

					$http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

						localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true });

						deferred.resolve(response);

					}).error(function (err, status) {
						_logOut();
						deferred.reject(err);
					});
				}
			}

			return deferred.promise;
		};

		var _logOut = function () {

			localStorageService.remove('authorizationData');

			_authentication.isAuth = false;
			_authentication.username = "";
			_authentication.useRefreshTokens = false;

		};


		var _fillAuthData = function () {

			var authData = localStorageService.get('authorizationData');
			if (authData) {
				_authentication.isAuth = true;
				_authentication.username = authData.userName;
				_authentication.useRefreshTokens = authData.useRefreshTokens;
			}

		};
		//authServiceFactory.saveRegistration = _saveRegistration;
		
		authServiceFactory.login = _login;
		authServiceFactory.logOut = _logOut;
		authServiceFactory.fillAuthData = _fillAuthData;
		authServiceFactory.authentication = _authentication;
		authServiceFactory.refreshToken = _refreshToken;
		//authServiceFactory.obtainAccessToken = _obtainAccessToken;
		//authServiceFactory.externalAuthData = _externalAuthData;
		//authServiceFactory.registerExternal = _registerExternal;

		return authServiceFactory;
	}])
	.service('uiLoad', ['$document', '$q', '$timeout', function ($document, $q, $timeout) {

		var loaded = [];
		var promise = false;
		var deferred = $q.defer();

		/**
		 * Chain loads the given sources
		 * @param srcs array, script or css
		 * @returns {*} Promise that will be resolved once the sources has been loaded.
		 */
		this.load = function (srcs) {
			srcs = angular.isArray(srcs) ? srcs : srcs.split(/\s+/);
			var self = this;
			if(!promise){
				promise = deferred.promise;
			}
	  angular.forEach(srcs, function(src) {
		promise = promise.then( function(){
			return src.indexOf('.css') >=0 ? self.loadCSS(src) : self.loadScript(src);
		} );
	  });
	  deferred.resolve();
	  return promise;
		}

		/**
		 * Dynamically loads the given script
		 * @param src The url of the script to load dynamically
		 * @returns {*} Promise that will be resolved once the script has been loaded.
		 */
		this.loadScript = function (src) {
			if(loaded[src]) return loaded[src].promise;

			var deferred = $q.defer();
			var script = $document[0].createElement('script');
			script.src = src;
			script.onload = function (e) {
				$timeout(function () {
					deferred.resolve(e);
				});
			};
			script.onerror = function (e) {
				$timeout(function () {
					deferred.reject(e);
				});
			};
			$document[0].body.appendChild(script);
			loaded[src] = deferred;

			return deferred.promise;
		};

		/**
		 * Dynamically loads the given CSS file
		 * @param href The url of the CSS to load dynamically
		 * @returns {*} Promise that will be resolved once the CSS file has been loaded.
		 */
		this.loadCSS = function (href) {
			if(loaded[href]) return loaded[href].promise;

			var deferred = $q.defer();
			var style = $document[0].createElement('link');
			style.rel = 'stylesheet';
			style.type = 'text/css';
			style.href = href;
			style.onload = function (e) {
				$timeout(function () {
					deferred.resolve(e);
				});
			};
			style.onerror = function (e) {
				$timeout(function () {
					deferred.reject(e);
				});
			};
			$document[0].head.appendChild(style);
			loaded[href] = deferred;

			return deferred.promise;
		};
}]);
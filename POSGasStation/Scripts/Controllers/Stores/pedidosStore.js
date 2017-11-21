angular.module("mainModule").controller("pedidosStore",["$scope","$http", "$location","userService", "orderService",  
  function($scope,$http, $location, userService, orderService) {
      $scope.listOrders;

        $scope.init = function(){
            var url = 'http://gsprest.azurewebsites.net/api/Ventas?idsuc='+userService.getSucursal();
            $http.get(url).then(function(msg){
                console.log(msg.data);
                $scope.listOrders= msg.data;
            });
        };
    }]);
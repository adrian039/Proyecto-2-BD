angular.module("mainModule").controller("contProveedorAdmin",["$scope","$http","userService",
function($scope,$http,userService) {
  $scope.providerList;

      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Proveedores';
        $scope.getHttp(url,(data)=>{
          this.providerList=data;
        });

      };

     
      $scope.updateProv = function(id,name,phone){
        var url='http://gsprest.azurewebsites.net/api/Proveedores/'+id;
        var send={
          "idproveedor": id,
          "nombre": name,
          "telefono": phone,
          "estado": 1,
        }
        $http.put(url,send)
        .then(
            function(response){
              // success callback
              console.log("update");
              $scope.init();
            }, 
            function(response){
              // failure callback
            }
         );
      };

      $scope.deleteProv=function(id){
        var url='http://gsprest.azurewebsites.net/api/Proveedores/'+id;
        $http.delete(url)
        .then(
            function(response){
              // success callback
              alert("Provider Deleted");
              $scope.init();
            }, 
            function(response){
              // failure callback
            }
         );
      }
      



    }]);
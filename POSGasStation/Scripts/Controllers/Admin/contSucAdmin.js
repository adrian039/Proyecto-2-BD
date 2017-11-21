angular.module("mainModule").controller("contSucAdmin", ["$scope","$http","$location"
,"userService","directionService",
function($scope,$http,$location,userService,directionService) {
  $scope.sucList;


  $scope.idSucursal;
  $scope.nombre;
  $scope.direccion;
  
      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Sucursales?idEmpresa='+userService.getCompany();
        $scope.getHttp(url,(data)=>{
          this.sucList=data;
        })

        
      };

      $scope.edit=function(){
        
        var url = "http://gsprest.azurewebsites.net/api/Sucursales/"+this.idSucursal;
        var sendData = {
          "idsucursal": parseInt(this.idSucursal),
          "nombre": this.nombre,
          "direccion": this.direccion,
          "imagen":globalImage,
          "estado":1,
          "idempresa":parseInt(userService.getCompany())
        }
        $http.put(url,sendData)
        .then(
          function(response){
              // success callback
              $scope.init();
              $location.path('/Admin/gsucursales');
              console.log("update");
            }, 
            function(response){
              // failure callback
            }
            );
      }

    

      $scope.delete=function(id){
        var url = 'http://gsprest.azurewebsites.net/api/Sucursales/'+id;
        
        $http.delete(url)
        .then(
            function(response){
              // success callback
              console.log("Se elimino");
              $scope.init();
            }, 
            function(response){
              // failure callback
            }
         );
      
    }

}]);

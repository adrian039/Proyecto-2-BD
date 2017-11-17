
angular.module("mainModule").controller("contMedNuevAdmin", ["$scope","$http",'$location',"userService",
function($scope,$http,$location,userService) {

  $scope.ean;
  $scope.idProveedor;
  $scope.nombre;
  $scope.descripcion;


  $scope.providerList;

      $scope.init = function(){
        var url = "http://gsprest.azurewebsites.net/api/Proveedores";
        $scope.getHttp(url,(data)=>{
          this.providerList=data;
        });
      }


      $scope.createProduct=function(){
        var url = "http://gsprest.azurewebsites.net/api/Productos";
        var sendData = {
          "ean": parseInt(this.ean),
          "nombre": this.nombre,
          "descripcion": this.descripcion,
          "imagen":globalImage,
          "idproveedor":parseInt(this.idProveedor.split(":",1)),
          "estado":1
        };

       $scope.postHttp(url,sendData,(data)=>{
          console.log("Data: "+data);
        });
        
      };

    }]);

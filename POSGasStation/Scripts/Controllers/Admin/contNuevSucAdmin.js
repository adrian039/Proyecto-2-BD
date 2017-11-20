angular.module("mainModule").controller("contNuevSucAdmin", ["$scope","$http","directionService",'$location',
'storeService','userService',

  function($scope,$http,directionService,$location,storeService,userService) {

    $scope.idSucursal;
    $scope.nombre;
    $scope.direccion;


    $scope.init = function(){


    }


    $scope.createStore=function(){
      var url = "http://gsprest.azurewebsites.net/api/Sucursales";
      var sendData = {
        "idsucursal": parseInt(this.idSucursal),
        "nombre": this.nombre,
        "direccion": this.direccion,
        "imagen":globalImage,
        "estado":1,
        "idempresa":parseInt(userService.getCompany())
      }

     $scope.postHttp(url,sendData,(data)=>{
        console.log("Data: "+data);
        alert("Store created");
      });
      
    }

  }]);
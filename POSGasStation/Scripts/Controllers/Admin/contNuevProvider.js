angular.module("mainModule").controller("contNuevProvider", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {
  $scope.name;
  $scope.id;
  $scope.number;

  $scope.craeteProvider=function(){
    var url = "http://gsprest.azurewebsites.net/api/Proveedores";
    var sendData={
    "nombre": name, 
    "telefono":number,
    "nombre":name,
    "estado":1
    };
    $scope.postHttp(url,sendData,(data)=>{
      console.log("Data: "+data);
    });
  };

      $scope.init = function(){

    }


}]);




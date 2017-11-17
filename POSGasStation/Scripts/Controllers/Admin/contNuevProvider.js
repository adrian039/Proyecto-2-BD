angular.module("mainModule").controller("contNuevProvider", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {

  $scope.createProv = function(id,name,phone){
    var url='http://gsprest.azurewebsites.net/api/Proveedores';
    var send={
      "idproveedor": id,
      "nombre": name,
      "telefono": phone,
      "estado": 1,
    }
    $scope.postHttp(url,send,(data)=>{
      console.log("Provider: "+data.nombre);
    })
  };




}]);




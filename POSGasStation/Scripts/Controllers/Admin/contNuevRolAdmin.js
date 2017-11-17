angular.module("mainModule").controller("contNuevRolAdmin", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {

  $scope.CreateRol=function(name,desc){
    var url='http://gsprest.azurewebsites.net/api/Roles';
    var sendData={
    "Nombre": name, 
    "Descripcion":desc,
    "Empresa":userService.getCompany(),
    "Estado":1
  };
    $http.post(url,sendData)
    .then(
      function successCallback(response){
        alert("Rol: "+name+" created");
        $location.path('/Admin/groles');
      },function errorCallBack(response){

      });
  };

}]);




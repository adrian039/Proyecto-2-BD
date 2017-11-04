angular.module("mainModule").controller("contNuevProvider", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {


  $scope.createRol=function(nme,descr){
    console.log('empresa: '+userService.getCompany());
    var url='http://'+getIp()+':58706/api/Roles';
    var sendData={
    "Nombre": nme, 
    "Descripcion":descr,
    "Empresa":userService.getCompany(),
    "Estado":1
  };
    $http.post(url,sendData)
    .then(
      function successCallback(response){
        alert("Rol: "+nme+" created");
        $location.path('/Admin/groles');
      },function errorCallBack(response){

      });
  };

      $scope.init = function(){

    }


}]);




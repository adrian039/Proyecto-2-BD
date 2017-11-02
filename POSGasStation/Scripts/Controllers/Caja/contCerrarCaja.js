angular.module("mainModule").controller("contCerrarCaja", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {
  $scope.idCash = "";
  $scope.amount="";

  $scope.openCash=function(){
    alert("Hola");
  };

      $scope.init = function(){
      alert("cerrado");
    };


}]);
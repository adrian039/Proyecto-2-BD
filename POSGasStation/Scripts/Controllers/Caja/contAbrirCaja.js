angular.module("mainModule").controller("contAbrirCaja", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {
  $scope.idCash = "";
  $scope.amount="";

  $scope.openCash=function(){
    alert("Hola");
  };

      $scope.init = function(){
      alert("hola");
    };


}]);
angular.module("mainModule").controller("contRegVenta", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {
  $scope.idClient = "";

  $scope.openCash=function(){
    alert("Entre");
  };

      $scope.init = function(){
    };


}]);
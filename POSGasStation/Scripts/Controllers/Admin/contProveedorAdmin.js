angular.module("mainModule").controller("contProveedorAdmin",["$scope","$http","userService",
function($scope,$http,userService) {
  $scope.providerList

      $scope.init = function(){
        console.log(userService.getCompany());
        var url='http://gsprest.azurewebsites.net/api/Proveedores?id='+userService.getCompany();
        $http.get(url).then(function(msg){
          console.log(msg);
        });

      };


      $scope.edit=function(){
      }

      $scope.delete=function(id,nme){
        var url = 'http://'+getIp()+':58706/api/Empleados';
        var data={
          "idEmpleado":id
        };
        $http({
          method: 'DELETE',
          url: url,
          data: data,
          headers: {
              'Content-type': 'application/json;charset=utf-8'
          }
      })
      .then(function(response) {
        alert("Employee "+nme+" fired");
        $scope.init();
      }, function(rejection) {
          console.log(rejection.data);
      });
      }
      



    }]);
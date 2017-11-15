angular.module("mainModule").controller("contEmplAdmin",["$scope","$http","userService",
function($scope,$http,userService) {
  $scope.employeelist;


      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Empleados?idEmpresa='+userService.getCompany();
        $scope.getHttp(url,(data)=>{
          this.employeelist=data;
        })

      };


      $scope.edit=function(){
      }

      $scope.delete=function(id,nme){
        
      }
    }]);
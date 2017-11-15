angular.module("mainModule").controller("contRolAdmin", ["$scope","$http",'userService',
function($scope,$http,userService) {
  $scope.rolList;
  

      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Roles'+userService.getCompany();
        $scope.getHttp(url,(data)=>{
          this.rolList=data;
        });

      };




      $scope.edit=function(id,name,desc){
    
      }
      $scope.delete=function(id,nme,desc){

      }
    }]);
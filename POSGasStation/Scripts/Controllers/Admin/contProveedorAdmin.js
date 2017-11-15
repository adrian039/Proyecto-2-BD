angular.module("mainModule").controller("contProveedorAdmin",["$scope","$http","userService",
function($scope,$http,userService) {
  $scope.providerList

      $scope.init = function(){
        console.log(userService.getCompany());
        var url='http://gsprest.azurewebsites.net/api/Proveedores?id='+userService.getCompany();
        $scope.getHttp(url,(data)=>{
          this.providerList=data;
        });

      };


      $scope.edit=function(){
      }

      $scope.delete=function(id,nme){
        \
      }
      



    }]);
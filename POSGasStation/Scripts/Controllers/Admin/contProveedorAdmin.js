angular.module("mainModule").controller("contProveedorAdmin",["$scope","$http","userService",
function($scope,$http,userService) {
  $scope.providerList;

      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Proveedores';
        $scope.getHttp(url,(data)=>{
          this.providerList=data;
        });

      };


      $scope.edit=function(){
      }

      $scope.delete=function(id,nme){
        
      }
      



    }]);
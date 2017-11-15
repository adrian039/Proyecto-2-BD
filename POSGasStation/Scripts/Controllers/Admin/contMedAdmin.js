angular.module("mainModule").controller("contMedAdmin",["$scope","$http","userService",
function($scope,$http,userService) {
  $scope.medList;


      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/ProductosSucursal?idSucursal='+userService.getSucursal();
        $scope.getHttp(url,(data)=>{
          this.medList=data;
        })

      }

      

      $scope.update=function(id,quantity,price){
        var url='http://'+getIp()+':58706/api/ProductoSucursal';
        var sendData={
          "idSucursal": userService.getSucursal(),
          "codProducto": id,
          "Cantidad": quantity,
          "Precio": price
        };
            $http.put(url,sendData)
            .then(function successCallback(data) {
              console.log("responde true: "+ data.data);
                console.log("pass verify")
            },
            function errorCallback(response) {
              alert("Reduce the number of "+product.Nombre);
              $location.path("/order");
              
             
            });

      }

      $scope.delete=function(id,nme){
        var url = 'http://'+getIp()+':58706/api/Productos';
        var data={
          "idProducto":id,
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
        alert("Product "+nme+" deleted");
        $scope.init();
      }, function(rejection) {
          console.log(rejection.data);
      });
      
    }


    }]);



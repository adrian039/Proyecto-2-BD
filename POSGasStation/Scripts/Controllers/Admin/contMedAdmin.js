angular.module("mainModule").controller("contMedAdmin",["$scope","$http","userService",
function($scope,$http,userService) {
  $scope.medList;
  $scope.providerList;

  $scope.ean;
  $scope.idProveedor;
  $scope.nombre;
  $scope.descripcion;


      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/ProductosSucursal?idSucursal='+userService.getSucursal();
        $scope.getHttp(url,(data)=>{
          this.medList=data;
        });

        url='http://gsprest.azurewebsites.net/api/Proveedores';
        $scope.getHttp(url,(data)=>{
          this.providerList=data;
        });

      }

      

      $scope.edit=function(){
        var url = "http://gsprest.azurewebsites.net/api/Productos/"+this.ean;
        var sendData={
        "ean": parseInt(this.ean),
        "nombre": this.nombre,
        "descripcion": this.descripcion,
        "imagen":globalImage,
        "idproveedor":parseInt(this.idProveedor.split(":",1)),
        "estado":1
      };
      $http.put(url,sendData)
      .then(
        function(response){
            // success callback
            $scope.init();
            
          }, 
          function(response){
            // failure callback
          }
          );

      }

      $scope.delete=function(id){
        var url = 'http://gsprest.azurewebsites.net/api/ProductosSucursal?prod='+id+'&suc='+userService.getSucursal();
        
        $http.delete(url)
        .then(
            function(response){
              // success callback
              console.log("se elimino");
              $scope.init();

            }, 
            function(response){
              // failure callback
            }
         );
      
    }


    }]);



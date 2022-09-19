app.directive("academicSelect", function () {
    return {
        templateUrl: "UI/components/academicSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.AcademicYearGetForDropDownList = [];
            $scope.defaultAcademicYearList = [];
            var academicObj = {
                Id : 0,
                AcademicYearCode : "Select Academic Year"
            }
            $scope.defaultAcademicYearList.push(academicObj);

            $scope.AcademicYearGetForDropDown = function () {
               
                $http({
                    method: 'POST',
                    url: 'api/IncAcademicYear/AcademicYearGetForDropdown',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                    
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                     
                            alert(response.obj);
                        }
                        else {
                      
                            $scope.tempList = response.obj;
                            $scope.AcademicYearGetForDropDownList = $scope.defaultAcademicYearList.concat($scope.tempList);
                           
                        }
                    })
                    .error(function (res) {
                   
                        alert(res.obj);
                    });
            }
            $scope.AcademicYearGetForDropDown();


        }],
        link: function (e, t, a) {
        }
    }
})
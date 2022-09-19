app.directive("institutewithfacultySelect", function () {
    return {
        templateUrl: "UI/components/institutewithfacultySelect.html",
        restrict: "E",
        replace: !0,
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstInstituteGetByFacIdAndInsTypeList = [];
            $scope.defaultInstitutesList = [];
            var institutesObj = {
                Id: 0,
                InstituteName: "Select Institute"
            }
            $scope.defaultInstitutesList.push(institutesObj);

            $scope.getMstInstituteGetByFacIdAndInsType = function (data) {
                $rootScope.showLoading = true;
           

                $http({
                    method: 'POST',
                    url: 'api/MstInstitute/MstInstituteGetByFacIdAndInsType',
                    data: { FacultyId : data },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.MstInstituteGetByFacIdAndInsTypeList = $scope.defaultInstitutesList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        $rootScope.showLoading = false;
                        alert(res.obj);
                    });
            }
        /*    $scope.getMstInstituteGetByFacIdAndInsType();*/

        }],
        link: function (e, t, a) {
        }
    }
})
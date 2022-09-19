app.directive("instituteSelect", function () {
    return {
        templateUrl: "UI/components/instituteSelect.html",
        restrict: "E",
        replace: !0,
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstInstituteGetForDropDownList = [];
            $scope.defaultInstituteList = [];
            var instituteObj = {
                Id: 0,
                InstituteName: "Select Institute"
            }
            $scope.defaultInstituteList.push(instituteObj);

            $scope.MstInstituteGetForDropDown = function () {

                $http({
                    method: 'POST',
                    url: 'api/MstInstitute/MstInstituteGetForDropDown',
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
                            $scope.MstInstituteGetForDropDownList = $scope.defaultInstituteList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.MstInstituteGetForDropDown();

        }],
        link: function (e, t, a) {
        }
    }
})
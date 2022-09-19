app.directive("examvenueSelect", function () {
    return {
        templateUrl: "UI/components/examvenueSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.ExamVenueListGetActiveList = [];
            $scope.defaultVenueList = [];
            var venueObj = {
                Id : 0,
                DisplayName: "Select Exam Venue"
            }
            $scope.defaultVenueList.push(venueObj);

            $scope.getExamVenueListGetActive = function (data) {
    
                $http({
                    method: 'POST',

                    url: 'api/ExamVenue/ExamVenueListGetActive',
                    data: { ExamCenterId : data},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                   
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                     
                            alert(response.obj);
                        }
                        else {
                        
                            $scope.tempList = response.obj;
                          
                            $scope.ExamVenueListGetActiveList = $scope.defaultVenueList.concat($scope.tempList);
           

                        }
                    })
                    .error(function (res) {
                   
                        alert(res.obj);
                    });
            }
      /*      $scope.getFacultyExamMapListGetActive();*/


        }],
        link: function (e, t, a) {
        }
    }
})
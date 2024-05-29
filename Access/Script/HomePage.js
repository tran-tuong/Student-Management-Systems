const text = "In the realm of modern education, efficient management of student data and course enrollments is essential for educational institutions to operate smoothly. The Student Management System (SMS) is a comprehensive software solution designed to streamline the administration of student records, course registrations, and enrollment processes. This system is pivotal for handling the complexities of academic management, providing a robust framework that supports the dynamic needs of educational institutions.";

let index = 0;
const typingSpeed = 35; // Adjust typing speed here (milliseconds)

function typeWriter() {
    if (index < text.length) {
        document.getElementById("typing-animation").innerHTML += text.charAt(index);
        index++;
        setTimeout(typeWriter, typingSpeed);
    }
}

// Call the typing function when the page loads
window.onload = function () {
    typeWriter();
};

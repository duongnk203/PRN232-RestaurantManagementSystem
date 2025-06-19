import axios from "axios";

// Cloudinary configuration from environment variables
const CLOUD_NAME = process.env.REACT_APP_CLOUDINARY_CLOUD_NAME;
const UPLOAD_PRESET = process.env.REACT_APP_CLOUDINARY_UPLOAD_PRESET;

// Validate configuration
if (!CLOUD_NAME || !UPLOAD_PRESET) {
  console.error(
    "Missing Cloudinary configuration. Please check your .env file."
  );
}

// Log configuration (remove in production)
console.log("Cloudinary Config:", {
  CLOUD_NAME,
  UPLOAD_PRESET,
});

export const cloudinaryUpload = async (file) => {
  if (!CLOUD_NAME || !UPLOAD_PRESET) {
    throw new Error(
      "Missing Cloudinary configuration. Please check your .env file."
    );
  }

  if (!file) {
    throw new Error("No file provided");
  }

  const formData = new FormData();
  formData.append("file", file);
  formData.append("upload_preset", UPLOAD_PRESET);
  formData.append("cloud_name", CLOUD_NAME);

  const url = `https://api.cloudinary.com/v1_1/${CLOUD_NAME}/image/upload`;

  try {
    const response = await axios.post(url, formData);
    if (!response.data || !response.data.secure_url) {
      throw new Error("Invalid response from Cloudinary");
    }
    return response.data.secure_url;
  } catch (error) {
    console.error(
      "Cloudinary upload error:",
      error.response?.data || error.message
    );
    throw new Error("Failed to upload image to Cloudinary. Please try again.");
  }
};

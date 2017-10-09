import cv2
import matplotlib.pyplot as plt

img_path = "../UnityDIP/Assets/Textures/link.jpg"
img = cv2.imread(img_path)
# img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
# cv2.imshow("Link", img)
# cv2.waitKey(0)

print("Open image with size " + str(img.shape))

plt.axis("off")
plt.imshow(cv2.cvtColor(img, cv2.COLOR_BGR2RGB))
plt.show()

# Show grayscale image
img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)

print("Open grayscale image with size " + str(img.shape))

plt.axis("off")
plt.imshow(img, cmap='gray')
plt.show()